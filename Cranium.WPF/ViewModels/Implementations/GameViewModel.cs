using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
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
