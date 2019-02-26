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
    public class GameService : AMongoModelService<Game>
    {
        #region FIELDS

        private const string CollectionName = "games";

        private static readonly TimeSpan timePerCycle = TimeSpan.FromMinutes(10);

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameService(IMongoDataServiceSettings settings, ICategoryService categoryService, IQuestionService questionService) 
            : base(settings, CollectionName)
        {
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Game Game { get; private set; }

        #endregion PROPERTIES


        #region METHODS

        public async Task CreateGameAsync(TimeSpan gameTime, IEnumerable<Player> players)
        {
            var cycleCount = gameTime.Seconds / timePerCycle.Seconds;
            await CreateGameAsync(cycleCount, players);
        }

        public async Task CreateGameAsync(int cycleCount, IEnumerable<Player> players)
        {
            var categories = await _categoryService.GetAsync();
            var tiles = CreateTiles(cycleCount, categories);
            var board = new GameBoard(tiles);

            var questions = await _questionService.GetAsync();

            Game = new Game(board, players, questions);
        }

        public async Task MovePlayerTo(ObjectId playerId, Color color)
        {
            Player player = null;
            foreach(var tile in Game.GameBoard)
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
                    var tileColor = await _categoryService.GetPropertyAsync(tile.CategoryId, x => x.Color);
                    if (tileColor == color)
                    {
                        tile.Players.Add(player);
                        return;
                    }
                }
            }
        }

        public Task<Question> GetQuestionForAsync(ObjectId playerId)
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

        public Task<bool> IsAtEnd(ObjectId playerId)
        {
            return Task.FromResult(Game.GameBoard.Last().Players.Any(x => x.Id == playerId));
        }

        private IEnumerable<Tile> CreateTiles(int cycleCount, IList<Category> categories)
        {
            if (categories == null || categories.Count == 0)
                throw new NoCategoriesException();

            var specialCategory = categories.FirstOrDefault(x => x.IsSpecial);
            if (specialCategory == null)
                throw new NoSpecialCategoryException();

            categories.RemoveWhere(x => x.IsSpecial);

            for (int i = 0; i < cycleCount; i++)
            {
                yield return new Tile(specialCategory.Id);

                foreach (var category in categories)
                    yield return new Tile(category.Id);
            }

            yield return new Tile(specialCategory.Id);
        }
        
        #endregion METHODS
    }
}
