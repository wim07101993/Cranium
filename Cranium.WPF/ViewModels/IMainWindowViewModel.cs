using Cranium.WPF.ViewModels.Data;

namespace Cranium.WPF.ViewModels
{
    public interface IMainWindowViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        IGameViewModel GameViewModel { get; }
        IDataViewModel DataViewModel { get; }
        ISettingsViewModel SettingsViewModel { get; }
    }
}
