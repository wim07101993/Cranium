using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class SettingsViewModel : AViewModelBase, ISettingsViewModel
    {
        public SettingsViewModel(IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
        }

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
