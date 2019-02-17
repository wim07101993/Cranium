namespace Cranium.WPF.ViewModels
{
    public interface ISettingsViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
