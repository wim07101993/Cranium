using System.Collections.Generic;
using System.Linq;
using Cranium.WPF.Game.Tile;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Unity;

namespace Cranium.WPF.Game.GameBoard
{
    public class GameBoardViewModel : AViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly IUnityContainer _unityContainer;


        public GameBoardViewModel(
            IStringsProvider stringsProvider, IGameService gameService, IUnityContainer unityContainer) : base(
            stringsProvider)
        {
            _gameService = gameService;
            _unityContainer = unityContainer;
            _gameService.GameChangedEvent += OnGameChanged;
        }


        public IReadOnlyList<TileViewModel> Tiles
            => _gameService
                ?.Game
                ?.GameBoard
                .Select(tile =>
                {
                    var tileViewModel = _unityContainer.Resolve<TileViewModel>();
                    tileViewModel.Model = tile;
                    return tileViewModel;
                })
                .ToList();


        private void OnGameChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged(nameof(Tiles));
        }
    }
}