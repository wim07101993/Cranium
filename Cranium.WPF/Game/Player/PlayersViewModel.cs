using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using MongoDB.Bson;
using Prism.Commands;
using Unity;
using Color = Cranium.WPF.Helpers.Color;

namespace Cranium.WPF.Game.Player
{
    public class PlayersViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IUnityContainer _unityContainer;
        private readonly IGameService _gameService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public PlayersViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            _gameService = unityContainer.Resolve<IGameService>();

            _gameService.Players.AutoUpdateCollection<Player, PlayerViewModel>(
                ItemsSource,
                NewPlayerViewModel,
                (player, playerViewModel) => player?.Id == playerViewModel?.Model?.Id);

            _gameService.Categories.AutoUpdateCollection<Category, Category>(
                Categories,
                x => x,
                (x, y) => x.Id == y.Id);

            CreateCommand = new DelegateCommand(() => { var _ = CreateAsync(); });
            DeleteCommand = new DelegateCommand<Player>(player => { var _ = DeleteAsync(player.Id); });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ObservableCollection<PlayerViewModel> ItemsSource { get; } = new ObservableCollection<PlayerViewModel>();

        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task CreateAsync()
        {
            var player = new Player
            {
                Color = new Color {BaseColor = Colors.MediumPurple},
                Name = Strings.NewPlayer
            };

            try
            {
                await _gameService.AddPlayersAsync(player);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async Task DeleteAsync(ObjectId player)
        {
            try
            {
                await _gameService.RemovePlayersAsync(player);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        private PlayerViewModel NewPlayerViewModel(Player player)
        {
            var playerViewModel = _unityContainer.Resolve<PlayerViewModel>();
            playerViewModel.Model = player;
            return playerViewModel;
        }
        
        #endregion METHODS
    }
}