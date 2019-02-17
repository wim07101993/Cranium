namespace Cranium.WPF.ViewModels
{
    public interface IHamburgerMenuViewModel : IViewModelBase
    {
        bool IsOpen { get; set; }
        int SelectedView { get; set; }
    }
}
