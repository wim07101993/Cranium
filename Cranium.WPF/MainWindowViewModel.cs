using Cranium.WPF.Data;
using Cranium.WPF.Game;
using Cranium.WPF.Game.GameControl;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Settings;
using Cranium.WPF.Strings;

namespace Cranium.WPF
{
    public class MainWindowViewModel : AViewModelBase
    {
        public MainWindowViewModel(
            IStringsProvider stringsProvider,
            HamburgerMenuViewModel hamburgerMenuViewModel, GameControlViewModel gameControlViewModel, DataViewModel dataViewModel,
            SettingsViewModel settingsViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameControlViewModel = gameControlViewModel;
            DataViewModel = dataViewModel;
            SettingsViewModel = settingsViewModel;
        }

        public HamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public GameControlViewModel GameControlViewModel { get; }
        public DataViewModel DataViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
    }
}