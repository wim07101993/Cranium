using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Events;
using Unity;

namespace Cranium.WPF.Game
{
    public class GameWindowViewModel : AViewModelBase
    {
        private QuestionViewModel _questionViewModel;
        private bool _showQuestion;

        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameWindowViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            GameBoardViewModel = unityContainer.Resolve<GameBoardViewModel>();
            GameService = unityContainer.Resolve<IGameService>();
            PlayersViewModel = unityContainer.Resolve<PlayersViewModel>();
            var eventAggregator = unityContainer.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<ShowQuestionEvent>().Subscribe(x => QuestionViewModel = x);
            eventAggregator.GetEvent<HideQuestionEvent>().Subscribe(() => QuestionViewModel = null);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public PlayersViewModel PlayersViewModel { get; }

        public QuestionViewModel QuestionViewModel
        {
            get => _questionViewModel;
            set => SetProperty(ref _questionViewModel, value);
        }

        public GameBoardViewModel GameBoardViewModel { get; }

        public IGameService GameService { get; }

        #endregion PROPERTIES


        #region METHODS
        
        #endregion METHODS
    }
}