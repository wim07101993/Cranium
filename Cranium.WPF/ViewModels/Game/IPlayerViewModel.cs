using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;

namespace Cranium.WPF.ViewModels.Game
{
    public interface IPlayerViewModel : IModelContainer<Player>
    {
        Category Category { get; set; }
        bool MoveBackwards { get; set; }
    }
}
