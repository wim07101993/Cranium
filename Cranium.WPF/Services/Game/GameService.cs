using Cranium.WPF.Exceptions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Extensions;
using System;
using MongoDB.Bson;
using Cranium.WPF.Services.Mongo.Implementations;

namespace Cranium.WPF.Services.Game
{
    public class GameService : AMongoModelService<Game>, IGameService
    {
        #region FIELDS

        private const string CollectionName = "games";


        private static readonly Random Random = new Random();
        private static readonly TimeSpan TimePerCycle = TimeSpan.FromMinutes(12);

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        private Game _game;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameService(
            IMongoDataServiceSettings settings, ICategoryService categoryService, IQuestionService questionService)
            : base(settings, CollectionName)
        {
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Game Game
        {
            get => _game;
            private set
            {
                _game = value;
                GameChangedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion PROPERTIES


        #region METHODS

        public async Task SaveGameAsync() => await CreateAsync(Game);

        public async Task<Game> LoadGameAsync(ObjectId gameId) => await GetOneAsync(gameId);

        public async Task<Game> CreateAsync(TimeSpan gameTime, IReadOnlyList<Player> players)
        {
            var cycleCount = gameTime.Seconds / TimePerCycle.Seconds;
            await CreateAsync(cycleCount, players);
            return Game;
        }

        public async Task<Game> CreateAsync(int cycleCount, IReadOnlyList<Player> players)
        {
            var questions = await _questionService.GetAsync();
            var categories = questions
                .Select(x => x.QuestionType.Category)
                .Distinct(new CategoryIdComparer())
                .ToList();

            var specialCategory = await _categoryService.GetByAsync(x => x.IsSpecial);
            categories.Add(specialCategory);

            var tiles = CreateTiles(cycleCount, categories);
            var board = new GameBoard(tiles);

            Game = new Game(board, players, questions) {CurrentPlayerIndex = Random.Next(players.Count)};
            return Game;
        }

        public async Task<Game> MovePlayerToAsync(ObjectId playerId, ObjectId categoryId)
        {
            Player player = null;
            foreach (var tile in Game.GameBoard)
            {
                if (player == null)
                {
                    var playerIndex = tile.Players.IndexOfFirst(x => x.Id == playerId);
                    if (playerIndex >= 0)
                    {
                        player = tile.Players[playerIndex];
                        tile.Players.RemoveAt(playerIndex);
                    }
                }
                else
                {
                    var dbCategoryId = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Id);
                    if (dbCategoryId == categoryId)
                    {
                        tile.Players.Add(player);
                        return Game;
                    }
                }
            }

            if (player == null)
                throw new PlayerNotFoundException();
            throw new TileNotFoundException();
        }

        public async Task<Game> MovePlayerBackwardsToAsync(ObjectId playerId, ObjectId categoryId)
        {
            Player player = null;

            var i = 0;
            for (; i < Game.GameBoard.Count; i++)
            {
                var tile = Game.GameBoard[i];
                var playerIndex = tile.Players.IndexOfFirst(x => x.Id == playerId);
                if (playerIndex < 0)
                    continue;

                player = tile.Players[playerIndex];
                tile.Players.RemoveAt(playerIndex);
            }

            if (player == null)
                throw new PlayerNotFoundException();

            for (; i >= 0; i--)
            {
                var tile = Game.GameBoard[i];
                var dbCategoryId = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Id);
                if (dbCategoryId == categoryId)
                {
                    tile.Players.Add(player);
                    return Game;
                }
            }
            
            throw new TileNotFoundException();
        }

        public Task<bool> IsAtEnd(ObjectId playerId)
        {
            return Task.FromResult(Game.GameBoard.Last().Players.Any(x => x.Id == playerId));
        }

        public async Task<Question> GetQuestionAsync()
            => await GetQuestionAsync(Game.TileOfCurrentPlayer.CategoryId);

        public async Task<Question> GetQuestionAsync(ObjectId categoryId)
        {
            var isSpecialCategory = await _categoryService.GetPropertyAsync(categoryId, x => x.IsSpecial);
            if (isSpecialCategory)
                return null;

            var question = Game.Questions.First(x => x.QuestionType.Category.Id == categoryId);
            Game.Questions.RemoveFirst(x => x.Id == question.Id);
            return question;
        }

        public async Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId)
        {
            var question = await _questionService.GetOneAsync(questionId);
            return question.Answers;
        }

        public Task NextTurnAsync()
        {
            Game.CurrentPlayerIndex =
                Game.CurrentPlayerIndex + 1 > Game.Players.Count
                    ? 0
                    : Game.CurrentPlayerIndex + 1;
            return Task.CompletedTask;
        }

        private static IEnumerable<Tile> CreateTiles(int cycleCount, IList<Category> categories)
        {
            if (categories == null || categories.Count == 0)
                throw new NoCategoriesException();

            var specialCategory = categories.FirstOrDefault(x => x.IsSpecial);
            if (specialCategory == null)
                throw new NoSpecialCategoryException();

            categories.RemoveWhere(x => x.IsSpecial);

            for (var i = 0; i < cycleCount; i++)
            {
                yield return new Tile(ObjectId.GenerateNewId(), specialCategory.Id);

                foreach (var category in categories)
                    yield return new Tile(ObjectId.GenerateNewId(), category.Id);
            }

            yield return new Tile(ObjectId.GenerateNewId(), specialCategory.Id);
        }

        #endregion METHODS


        #region EVENTS

        public event EventHandler GameChangedEvent;
        public event EventHandler PlayerChangedEvent;

        #endregion EVENTS


        #region CLASSES

        private class CategoryIdComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y) => x != null && x.Id == y?.Id;

            public int GetHashCode(Category category) => category.Id.GetHashCode();
        }

        #endregion CLASSES
    }
}