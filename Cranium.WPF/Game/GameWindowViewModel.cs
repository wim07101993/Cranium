using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Unity;

namespace Cranium.WPF.Game
{
    public class GameWindowViewModel : AViewModelBase
    {
        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameWindowViewModel(
            IUnityContainer unityContainer, QuestionViewModel questionViewModel, GameBoardViewModel gameBoardViewModel, IGameService gameService)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            QuestionViewModel = questionViewModel;
            GameBoardViewModel = gameBoardViewModel;
            GameService = gameService;
            PlayersViewModel = unityContainer.Resolve<PlayersViewModel>();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public PlayersViewModel PlayersViewModel { get; }

        public QuestionViewModel QuestionViewModel { get; }

        public GameBoardViewModel GameBoardViewModel { get; }

        public IGameService GameService { get; }

        #endregion PROPERTIES


        #region METHODS

        #endregion METHODS
    }
}