using Cranium.WPF.Exceptions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Extensions;
using System;

namespace Cranium.WPF.Services.Game
{
    public class GameService
    {
        #region FIELDS

        private static readonly TimeSpan timePerCycle = TimeSpan.FromMinutes(10);

        private readonly ICategoryService _categoryService;
        private readonly IQuestionService _questionService;
        
        #endregion FIELDS


        #region CONSTRUCTORS

        public GameService(ICategoryService categoryService, IQuestionService questionService)
        {
            _categoryService = categoryService;
            _questionService = questionService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES



        #endregion PROPERTIES


        #region METHODS

        public async Task<Game> CreateGameAsync(TimeSpan gameTime, IEnumerable<Player> players)
        {
            var cycleCount = gameTime.Seconds / timePerCycle.Seconds;
            var categories = await _categoryService.GetAsync();
            var tiles = CreateTiles(cycleCount, categories);
            var board = new GameBoard(tiles);

            return new Game(board, players);
        }

        public async Task<Game> CreateGameAsync(int cycleCount, IEnumerable<Player> players)
        {
            var categories = await _categoryService.GetAsync();
            var tiles = CreateTiles(cycleCount, categories);
            var board = new GameBoard(tiles);

            return new Game(board, players);
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
