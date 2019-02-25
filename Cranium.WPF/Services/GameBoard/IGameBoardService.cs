using Cranium.WPF.Models;
using System;
using System.Threading.Tasks;

namespace Cranium.WPF.Services.GameBoard
{
    public interface IGameBoardService
    {
        Task<Tile> GenerateGameBoardAsync(int cycleCount);
        Task<Tile> GenerateGameBoardAsync(TimeSpan gameTime);
    }
}
