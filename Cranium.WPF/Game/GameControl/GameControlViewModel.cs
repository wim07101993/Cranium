using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;

namespace Cranium.WPF.Game.GameControl
{
    public class GameControlViewModel : AViewModelBase
    {
        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameControlViewModel(
            IStringsProvider stringsProvider, HamburgerMenuViewModel hamburgerMenuViewModel,
            GameBoardViewModel gameBoardViewModel, QuestionViewModel questionViewModel, IGameService gameService,
            PlayersViewModel playersViewModel)
            : base(stringsProvider)
        {
            GameService = gameService;
            PlayersViewModel = playersViewModel;

            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameBoardViewModel = gameBoardViewModel;
            QuestionViewModel = questionViewModel;

            CreateGameCommand = new DelegateCommand<double?>(async x =>
            {
                var gameTime = x != null
                    ? (int) x
                    : 0;
                if (gameTime > 0)
                    await CreateGameAsync(gameTime);
            });

            StartCommand = new DelegateCommand(async () => await StartAsync());
            StopCommand = new DelegateCommand(async () => await StopAsync());
            RestartCommand = new DelegateCommand(async () => await RestartAsync());
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IGameService GameService { get; }

        public HamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public GameBoardViewModel GameBoardViewModel { get; }
        public QuestionViewModel QuestionViewModel { get; }
        public PlayersViewModel PlayersViewModel { get; }

        public ICommand CreateGameCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        public ICommand RestartCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        private async Task CreateGameAsync(int gameTime)
            => await GameService.CreateAsync(TimeSpan.FromMinutes(gameTime));

        private async Task StartAsync()
            => await GameService.StartGameAsync();

        private async Task StopAsync()
            => await GameService.StopGameAsync();

        private async Task RestartAsync()
            => await GameService.CreateAsync(GameService.GameBoard.Count / GameService.Categories.Count);

        #endregion METHODS
    }
}