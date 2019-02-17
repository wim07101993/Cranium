namespace Cranium.WPF.ViewModels
{
    public interface IGameViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
