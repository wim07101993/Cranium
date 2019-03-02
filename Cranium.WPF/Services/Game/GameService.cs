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

        public async Task<Game> CreateAsync(TimeSpan gameTime, IEnumerable<Player> players)
        {
            var cycleCount = gameTime.Seconds / TimePerCycle.Seconds;
            await CreateAsync(cycleCount, players);
            return Game;
        }

        public async Task<Game> CreateAsync(int cycleCount, IEnumerable<Player> players)
        {
            var categories = await _categoryService.GetAsync();
            var tiles = CreateTiles(cycleCount, categories);
            var board = new GameBoard(tiles);

            var questions = await _questionService.GetAsync();

            Game = new Game(board, players, questions);
            return Game;
        }

        public async Task<Game> MovePlayerTo(ObjectId playerId, ObjectId categoryId)
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
                    var dbCtegoryId = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Id);
                    if (dbCtegoryId == categoryId)
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

        public Task<bool> IsAtEnd(ObjectId playerId)
        {
            return Task.FromResult(Game.GameBoard.Last().Players.Any(x => x.Id == playerId));
        }

        public Task<Question> GetQuestionAsync(ObjectId playerId)
        {
            foreach (var tile in Game.GameBoard)
                if (tile.Players.Any(x => x.Id == playerId))
                    return Task.FromResult(Game.Questions.First(x => x.QuestionType.Category.Id == tile.CategoryId));

            throw new PlayerNotFoundException();
        }

        public async Task<IEnumerable<Answer>> GetAnswers(ObjectId questionId)
        {
            var question = await _questionService.GetOneAsync(questionId);
            return question.Answers;
        }

        private static IEnumerable<Tile> CreateTiles(int cycleCount, IList<Category> categories)
        {
            if (categories == null || categories.Count == 0)
                throw new NoCategoriesException();

            var specialCategory = categories.FirstOrDefault(x => x.IsSpecial);
            if (specialCategory == null)
                throw new NoSpecialCategoryException();

            categories.RemoveWhere(x => x.IsSpecial);

            for (int i = 0; i < cycleCount; i++)
            {
                yield return new Tile(ObjectId.GenerateNewId(),  specialCategory.Id);

                foreach (var category in categories)
                    yield return new Tile(ObjectId.GenerateNewId(), category.Id);
            }

            yield return new Tile(ObjectId.GenerateNewId(), specialCategory.Id);
        }

        #endregion METHODS


        #region EVENTS

        public event EventHandler GameChangedEvent;

        #endregion EVENTS
    }
}