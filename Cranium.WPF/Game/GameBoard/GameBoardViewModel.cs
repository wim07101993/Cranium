using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cranium.WPF.Game.Tile;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Unity;

namespace Cranium.WPF.Game.GameBoard
{
    public class GameBoardViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IGameService _gameService;
        private readonly IUnityContainer _unityContainer;

        private double _tileWidth = 120;
        private double _tileHeight = 80;

        private double _playerSize;
        private double _playerMargin;

        #endregion FIELDS


        #region CONSTRCUTOR

        public GameBoardViewModel(
            IStringsProvider stringsProvider, IGameService gameService, IUnityContainer unityContainer) : base(
            stringsProvider)
        {
            _gameService = gameService;
            _unityContainer = unityContainer;
            _gameService.GameChanged += OnGameChanged;
            ((INotifyCollectionChanged)_gameService.Players).CollectionChanged += (s, e) => RecalculatePlayerSize();
            RecalculatePlayerSize();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IReadOnlyList<TileViewModel> Tiles
            => _gameService
                .GameBoard
                ?.Select(tile =>
                {
                    var tileViewModel = _unityContainer.Resolve<TileViewModel>();
                    tileViewModel.Model = tile;
                    return tileViewModel;
                })
                .ToList();

        public double TileWidth
        {
            get => _tileWidth;
            set
            {
                if (!SetProperty(ref _tileWidth, value))
                    return;

                RecalculatePlayerSize();
            }
        }

        public double TileHeight
        {
            get => _tileHeight;
            set
            {
                if (!SetProperty(ref _tileHeight, value))
                    return;

                RecalculatePlayerSize();
            }
        }

        public double PlayerSize
        {
            get => _playerSize;
            private set => SetProperty(ref _playerSize, value);
        }

        public double PlayerMargin
        {
            get => _playerMargin;
            private set => SetProperty(ref _playerMargin, value);
        }

        #endregion PROPERTIES

        
        #region METHODS

        private Task OnGameChanged(object sender)
        {
            RaisePropertyChanged(nameof(Tiles));
            return Task.CompletedTask;
        }

        private void RecalculatePlayerSize()
        {
            var playerCount = _gameService.Players.Count;
            if (playerCount < 1)
                return;

            var width = Math.Max(TileWidth, TileHeight);
            var height = Math.Min(TileHeight, TileWidth);
            var columns = playerCount;
            var rows = 1;

            while (width /columns < height/rows)
            {
                rows++;
                columns = (int)Math.Ceiling((double)playerCount / rows);
            }

            var playerContainerSize = Math.Min(width / columns, height / rows);
            PlayerMargin = playerContainerSize / 10;
            PlayerSize = playerContainerSize - PlayerMargin * 2;
        }
        
        #endregion METHODS
    }
}