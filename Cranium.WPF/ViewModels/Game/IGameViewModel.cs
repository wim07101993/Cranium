namespace Cranium.WPF.ViewModels.Game
{
    public interface IGameViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        IGameBoardViewModel GameBoardViewModel { get; }

        Services.Game.Game Game { get; }
    }
}