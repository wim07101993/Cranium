using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.GameControl;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Game.Question;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Events;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Cranium.WPF.Game
{
    public class GameWindowViewModel : AViewModelBase
    {
        #region FIELDS

        private QuestionViewModel _questionViewModel;
        private Player.Player _winner;
        private TimeSpan _time;
        private bool _showTime;
        private Stopwatch _stopwatch = new Stopwatch();
        private CancellationTokenSource _cancellationTokenSource;

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
            eventAggregator.GetEvent<ShowWinnerEvent>().Subscribe(x => Winner = x);
            eventAggregator.GetEvent<HideWinnerEvent>().Subscribe(() => Winner = null);
            eventAggregator.GetEvent<StartTimerEvent>().Subscribe(x =>
            {
                var _ = StartTimerAsync(x);
            });
            eventAggregator.GetEvent<StopTimerEvent>().Subscribe(StopTimer);
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

        public Player.Player Winner
        {
            get => _winner;
            set => SetProperty(ref _winner, value);
        }

        public TimeSpan Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public bool ShowTime
        {
            get => _showTime;
            set => SetProperty(ref _showTime, value);
        }

        #endregion PROPERTIES


        #region METHODS

        public async Task StartTimerAsync(TimeSpan time)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            await Task.Factory.StartNew(() =>
            {
                _stopwatch.Stop();
                _stopwatch.Reset();

                ShowTime = true;
                _stopwatch.Start();

                while (_stopwatch.ElapsedTicks < time.Ticks)
                {
                    Time = time - TimeSpan.FromTicks(_stopwatch.ElapsedTicks);
                }
                Time = new TimeSpan();
            }, _cancellationTokenSource.Token);
        }

        public void StopTimer()
        {
            _cancellationTokenSource.Cancel();
            ShowTime = false;
            Time = default;
        }

        #endregion METHODS
    }
}