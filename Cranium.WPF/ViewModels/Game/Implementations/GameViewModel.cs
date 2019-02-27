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

        private Services.Game.Game _game;
        private List<Player> _players;

        #endregion FIELDS


        #region CONSTRUCTOR

        public GameViewModel(
            IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel, IGameService gameService)
            : base(stringsProvider)
        {
            _gameService = gameService;
            HamburgerMenuViewModel = hamburgerMenuViewModel;

            var _ = InitAsync();
        }

        public async Task InitAsync()
        {
            Players = new List<Player>()
            {
                new Player
                {
                    Color = new Color {BaseColor = Colors.Red}
                },
                new Player
                {
                    Color = new Color {BaseColor = Colors.Blue}
                },
                new Player
                {
                    Color = new Color {BaseColor = Colors.Green}
                },
                new Player
                {
                    Color = new Color {BaseColor = Colors.Yellow}
                }
            };

            Game = await _gameService.CreateAsync(4, Players);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }

        public List<Player> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        public Services.Game.Game Game
        {
            get => _game;
            set => SetProperty(ref _game, value);
        }

        #endregion PROPERTIES


        #region METHODS

        #endregion METHODS
    }
}