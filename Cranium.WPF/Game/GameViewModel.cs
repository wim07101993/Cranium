using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Question;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using MongoDB.Bson;
using Color = Cranium.WPF.Helpers.Color;

namespace Cranium.WPF.Game
{
    public class GameViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IGameService _gameService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameViewModel(
            IStringsProvider stringsProvider, HamburgerMenuViewModel hamburgerMenuViewModel,
            GameBoardViewModel gameBoardViewModel, QuestionViewModel questionViewModel, IGameService gameService)
            : base(stringsProvider)
        {
            _gameService = gameService;
            _gameService.GameChangedEvent += OnGameChanged;

            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameBoardViewModel = gameBoardViewModel;
            QuestionViewModel = questionViewModel;

            var _ = InitAsync();
        }

        public async Task InitAsync()
        {
            var players = new List<Player.Player>
            {
                new Player.Player {Color = new Color {BaseColor = Colors.Red}, Id = ObjectId.GenerateNewId()},
                new Player.Player {Color = new Color {BaseColor = Colors.Blue}, Id = ObjectId.GenerateNewId()},
                new Player.Player {Color = new Color {BaseColor = Colors.Green}, Id = ObjectId.GenerateNewId()},
                new Player.Player {Color = new Color {BaseColor = Colors.Yellow}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.Purple}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.LightBlue}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.LightGreen}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.Pink}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.Orange}, Id = ObjectId.GenerateNewId()},
//                new Player {Color = new Color {BaseColor = Colors.Violet}, Id = ObjectId.GenerateNewId()},
            };

            try
            {
                await _gameService.CreateAsync(5, players);
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

        public WPF.Game.Game Game => _gameService.Game;
        public Player.Player CurrentPlayer => _gameService.Game?.CurrentPlayer;

        #endregion PROPERTIES


        #region METHODS

        private void OnGameChanged(object sender, EventArgs e)
        {
            Game.PropertyChanged += OnGamePropertyChanged;
            RaisePropertyChanged(nameof(Game));
            RaisePropertyChanged(nameof(CurrentPlayer));
        }

        private void OnGamePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(WPF.Game.Game.CurrentPlayer):
                    RaisePropertyChanged(nameof(CurrentPlayer));
                    break;
            }
        }

        #endregion METHODS
    }
}