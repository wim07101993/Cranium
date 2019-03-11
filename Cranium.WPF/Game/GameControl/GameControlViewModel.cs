using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;
using Prism.Events;

namespace Cranium.WPF.Game.GameControl
{
    public class GameControlViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IEventAggregator _eventAggregator;
        private bool _showDice;
        private TimeSpan _time;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameControlViewModel(
            IStringsProvider stringsProvider, HamburgerMenuViewModel hamburgerMenuViewModel,
            GameBoardViewModel gameBoardViewModel, QuestionViewModel questionViewModel, IGameService gameService,
            PlayersViewModel playersViewModel, IEventAggregator eventAggregator)
            : base(stringsProvider)
        {
            _eventAggregator = eventAggregator;

            GameService = gameService;
            GameService.Categories.Sync<Category, Category>(Categories, x => x, (x, y) => x.Id == y.Id);
            GameService.GameFinished += OnGameFinishedAsync;
            GameService.GameChanged += OnGameChanged;

            PlayersViewModel = playersViewModel;
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameBoardViewModel = gameBoardViewModel;
            QuestionViewModel = questionViewModel;
            QuestionViewModel.QuestionAnswered += OnQuestionAnswered;

            CreateGameCommand = new DelegateCommand<double?>(x =>
            {
                var gameTime = x != null
                    ? (int) x
                    : 0;
                if (gameTime > 0)
                {
                    var _ = CreateGameAsync(gameTime);
                }
            });

            StartCommand = new DelegateCommand(() =>
            {
                var _ = StartAsync();
            });
            StopCommand = new DelegateCommand(() =>
            {
                var _ = StopAsync();
            });
            RestartCommand = new DelegateCommand(() =>
            {
                var _ = RestartAsync();
            });

            MovePlayerToCommand = new DelegateCommand<Category>(x =>
            {
                var _ = MovePlayerToAsync(x);
            });

            StartTimerCommand = new DelegateCommand(StartTimer);
            StopTimerCommand = new DelegateCommand(StopTimer);
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

        public ICommand MovePlayerToCommand { get; }

        public bool ShowDice
        {
            get => _showDice;
            set => SetProperty(ref _showDice, value);
        }

        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public ICommand StartTimerCommand { get; }
        public ICommand StopTimerCommand { get; }

        public TimeSpan Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        #endregion PROPERTIES


        #region METHODS

        private async Task CreateGameAsync(int gameTime)
        {
            try
            {
                await GameService.CreateAsync(TimeSpan.FromMinutes(gameTime));
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task StartAsync()
        {
            try
            {
                await GameService.StartGameAsync();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task StopAsync()
        {
            try
            {
                await GameService.StopGameAsync();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task RestartAsync()
        {
            try
            {
                await GameService.RestartGameAsync();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private async Task OnQuestionAnswered(QuestionViewModel sender)
        {
            if (sender.IsAnswerCorrect)
                ShowDice |= GameService.CurrentPlayer != null;
            else
                await GameService.NextTurnAsync();
        }

        private async Task MovePlayerToAsync(Category category)
        {
            ShowDice = false;
            try
            {
                await GameService.MovePlayerToAsync(GameService.CurrentPlayer.Id, category.Id);
                await GameService.NextTurnAsync();
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private Task OnGameFinishedAsync(IGameService gameService, Player.Player winner)
        {
            _eventAggregator.GetEvent<ShowWinnerEvent>().Publish(winner);
            return Task.CompletedTask;
        }

        private Task OnGameChanged(IGameService sender)
        {
            _eventAggregator.GetEvent<HideWinnerEvent>().Publish();
            return Task.CompletedTask;
        }

        private void StartTimer()
        {
            _eventAggregator.GetEvent<StartTimerEvent>().Publish(Time);
        }

        private void StopTimer()
        {
            _eventAggregator.GetEvent<StopTimerEvent>().Publish();
        }

        #endregion METHODS
    }
}