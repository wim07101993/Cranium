using Cranium.Data.Models;
using System;
using System.Threading.Tasks;

namespace Cranium.Data.Services
{
    public interface IGameBoardService
    {
        Task<Tiles> GenerateTilesAsync(int countOfCycles);
        Task<Tiles> GenerateTilesAsync(TimeSpan gameTime);
    }
}
