using Cranium.WPF.Services.Game;

namespace Cranium.WPF.ViewModels.Game
{
    public interface IGameViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        IGameBoardViewModel GameBoardViewModel { get; }
        IQuestionViewModel QuestionViewModel { get; }

        Services.Game.Game Game { get; }
        Player CurrentPlayer { get; }
    }
}