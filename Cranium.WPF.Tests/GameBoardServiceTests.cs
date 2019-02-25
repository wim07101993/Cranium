using Cranium.WPF.Services.GameBoard;
using Cranium.WPF.Services.Mongo.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cranium.WPF.Tests
{
    [TestClass]
    public class GameBoardServiceTests
    {
        [TestMethod]
        public async void GenerateGameBoardAsync()
        {
            var gameboardService = new GameBoardService(new MockService());

            var tile = gameboardService.GenerateGameBoardAsync(2);
        }
    }
}
