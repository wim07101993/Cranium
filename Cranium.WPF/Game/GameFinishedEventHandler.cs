using System.Threading.Tasks;

namespace Cranium.WPF.Game
{
    public delegate Task AsyncGameFinishedEventHandler(IGameService gameService, Player.Player winner);
}
