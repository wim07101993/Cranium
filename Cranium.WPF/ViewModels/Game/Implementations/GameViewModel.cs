using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
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
            IGameBoardViewModel gameBoardViewModel, IGameService gameService)
            : base(stringsProvider)
        {
            _gameService = gameService;
            _gameService.GameChangedEvent += OnGameChanged;
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            GameBoardViewModel = gameBoardViewModel;

            var _ = InitAsync();
        }

        public async Task InitAsync()
        {
            var players = new List<Player>
            {
                new Player{Color = new Color {BaseColor = Colors.Red}},
                new Player{Color = new Color {BaseColor = Colors.Blue}},
                new Player{Color = new Color {BaseColor = Colors.Green}},
                new Player{Color = new Color {BaseColor = Colors.Yellow}},
//                new Player {Color = new Color {BaseColor = Colors.Purple}},
//                new Player {Color = new Color {BaseColor = Colors.LightBlue}},
//                new Player {Color = new Color {BaseColor = Colors.LightGreen}},
//                new Player {Color = new Color {BaseColor = Colors.Pink}},
//                new Player {Color = new Color {BaseColor = Colors.Orange}},
//                new Player {Color = new Color {BaseColor = Colors.Violet}},
            };

            await _gameService.CreateAsync(5, players);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public IGameBoardViewModel GameBoardViewModel { get; }

        public Services.Game.Game Game => _gameService.Game;

        #endregion PROPERTIES


        #region METHODS

        private void OnGameChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Game));
        }

        #endregion METHODS
    }
}