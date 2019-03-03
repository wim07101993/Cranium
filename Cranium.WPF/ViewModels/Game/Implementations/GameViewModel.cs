using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using MongoDB.Bson;
using Color = Cranium.WPF.Models.Color;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class GameViewModel : AViewModelBase, IGameViewModel
    {
        #region FIELDS

        private readonly IGameService _gameService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameViewModel(
            IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel,
            IGameBoardViewModel gameBoardViewModel, IQuestionViewModel questionViewModel, IGameService gameService)
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
            var players = new List<Player>
            {
                new Player {Color = new Color {BaseColor = Colors.Red}, Id = ObjectId.GenerateNewId()},
                new Player {Color = new Color {BaseColor = Colors.Blue}, Id = ObjectId.GenerateNewId()},
                new Player {Color = new Color {BaseColor = Colors.Green}, Id = ObjectId.GenerateNewId()},
                new Player {Color = new Color {BaseColor = Colors.Yellow}, Id = ObjectId.GenerateNewId()},
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

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public IGameBoardViewModel GameBoardViewModel { get; }
        public IQuestionViewModel QuestionViewModel { get; }

        public Services.Game.Game Game => _gameService.Game;
        public Player CurrentPlayer => _gameService.Game?.CurrentPlayer;

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
                case nameof(Services.Game.Game.CurrentPlayer):
                    RaisePropertyChanged(nameof(CurrentPlayer));
                    break;
            }
        }

        #endregion METHODS
    }
}