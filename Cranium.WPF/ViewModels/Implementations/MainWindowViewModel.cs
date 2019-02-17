using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class MainWindowViewModel : AViewModelBase, IMainWindowViewModel
    {
        public MainWindowViewModel(
            IStringsProvider stringsProvider,
            IHamburgerMenuViewModel hamburgerMenuViewModel, IGameViewModel gameViewModel, IDataViewModel dataViewModel,
            ISettingsViewModel settingsViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameViewModel = gameViewModel;
            DataViewModel = dataViewModel;
            SettingsViewModel = settingsViewModel;
        }

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public IGameViewModel GameViewModel { get; }
        public IDataViewModel DataViewModel { get; }
        public ISettingsViewModel SettingsViewModel { get; }
    }
}