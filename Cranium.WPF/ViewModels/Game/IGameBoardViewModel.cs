using System.Collections.Generic;

namespace Cranium.WPF.ViewModels.Game
{
    public interface IGameBoardViewModel : IViewModelBase
    {
        IReadOnlyList<ITileViewModel> Tiles { get; }
    }
}
