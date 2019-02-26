using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class GameViewModel : AViewModelBase, IGameViewModel
    {
        public GameViewModel(IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
        }

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
