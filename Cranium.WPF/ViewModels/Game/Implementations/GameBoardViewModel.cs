using System.Collections.Generic;
using System.Linq;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Unity;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class GameBoardViewModel : AViewModelBase, IGameBoardViewModel
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


        public IReadOnlyList<ITileViewModel> Tiles
            => _gameService
                ?.Game
                ?.GameBoard
                .Select(tile =>
                {
                    var tileViewModel = _unityContainer.Resolve<ITileViewModel>();
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