using Cranium.WPF.Services.GameBoard;
using Cranium.WPF.Services.Mongo.Mocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Cranium.WPF.Tests
{
    [TestClass]
    public class GameBoardServiceTests
    {
        [TestMethod]
        public async Task GenerateGameBoardAsync()
        {
            var gameboardService = new GameBoardService(new MockService());

            var tile = await gameboardService.GenerateGameBoardAsync(2);

            for (int i = 0; i < 10; i++)
            {
                var nextTile = tile.NextTiles.FirstOrDefault();
                nextTile.Should().NotBeNull();
                tile = nextTile;
            }
        }
    }
}
