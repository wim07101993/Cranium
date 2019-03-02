using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;

namespace Cranium.WPF.ViewModels.Game
{
    public interface ITileViewModel : IModelContainer<Tile>
    {
        Category Category { get; }
    }
}
