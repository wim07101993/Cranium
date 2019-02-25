using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;

namespace Cranium.WPF.Services.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        #region FIELDS

        private static readonly TimeSpan _timePerTile = TimeSpan.FromMinutes(5);

        private readonly ICategoryService _categoryService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameBoardService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion CONSTRUCTOR


        #region METHODS

        public async Task<Tile> GenerateGameBoardAsync(int cycleCount)
        {
            if (cycleCount <= 0)
                return null;

            var categories = await _categoryService.GetAsync(new Expression<Func<Category, object>>[] { x => x.Id });
            if (categories == null || categories.Count <= 0)
                return null;

            var originalTile = new Tile();
            var tile = originalTile;
            for (int i = 0; i < cycleCount; i++)
            {
                foreach (var categoryId in categories.Select(x => x.Id))
                {
                    var nextTile = new Tile { CategoryId = categoryId };
                    tile.NextTiles = new List<Tile> { nextTile };
                    tile = nextTile;
                }
            }

            return originalTile;
        }

        public async Task<Tile> GenerateGameBoardAsync(TimeSpan gameTime)
        {
            var tileCount = gameTime.Milliseconds / _timePerTile.Milliseconds;
            if (tileCount == 0)
                return null;

            var categories = await _categoryService.GetAsync();
            if (categories == null || categories.Count <= 0)
                return null;

            // add one because of the purple tile
            var categoryCount = categories.Count + 1;

            return await GenerateGameBoardAsync(tileCount / categoryCount);
        }

        #endregion METHODS
    }
}
