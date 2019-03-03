using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Settings
{
    public class SettingsViewModel : AViewModelBase
    {
        public SettingsViewModel(IStringsProvider stringsProvider, HamburgerMenuViewModel hamburgerMenuViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
        }

        public HamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
