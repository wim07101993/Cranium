using System;
using System.Threading.Tasks;
using Cranium.Data.Models;

namespace Cranium.Data.Services
{
    public class GameBoardService : IGameBoardService
    {
        private readonly IDataService _dataService;

        public GameBoardService(IDataService dataService)
        {
            _dataService = dataService;
        }


        public async Task<Tiles> GenerateTilesAsync(int countOfCycles)
        {
            var categories = await _dataService.GetCategoriesAsync();

            throw new NotImplementedException();

        }

        public async Task<Tiles> GenerateTilesAsync(TimeSpan gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
