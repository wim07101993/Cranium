using System;
using System.Threading.Tasks;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Game.GameControl
{
    public class GameControlViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IGameService _gameService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameControlViewModel(
            IStringsProvider stringsProvider, HamburgerMenuViewModel hamburgerMenuViewModel,
            GameBoardViewModel gameBoardViewModel, QuestionViewModel questionViewModel, IGameService gameService, PlayersViewModel playersViewModel)
            : base(stringsProvider)
        {
            _gameService = gameService;
            PlayersViewModel = playersViewModel;
            _gameService.PlayerChanged += OnPlayerChangedAsync;

            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameBoardViewModel = gameBoardViewModel;
            QuestionViewModel = questionViewModel;

            var _ = InitAsync();
        }

        public async Task InitAsync()
        {
            try
            {
                await _gameService.CreateAsync(5);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public HamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public GameBoardViewModel GameBoardViewModel { get; }
        public QuestionViewModel QuestionViewModel { get; }
        public PlayersViewModel PlayersViewModel { get; }

        public Player.Player CurrentPlayer => _gameService.CurrentPlayer;

        #endregion PROPERTIES


        #region METHODS

        private Task OnPlayerChangedAsync(object sender)
        {
            RaisePropertyChanged(nameof(CurrentPlayer));
            return Task.CompletedTask;
        }

        #endregion METHODS
    }
}