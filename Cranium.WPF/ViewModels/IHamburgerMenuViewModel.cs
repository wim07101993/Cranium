namespace Cranium.WPF.ViewModels
{
    public interface IHamburgerMenuViewModel : IViewModelBase
    {
        bool IsOpen { get; set; }
        string SelectedView { get; set; }
    }
}
