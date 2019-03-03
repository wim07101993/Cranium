using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class PlayerViewModel : AViewModelBase, IPlayerViewModel
    {
        #region FIELDS

        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        private Player _model;
        private Category _category;
        private bool _isUpdatingCategory;
        private bool _isUpdatingModel;
        private bool _moveBackwards;

        #endregion FIELDS


        #region CONSTRUCTOR

        public PlayerViewModel(
            IStringsProvider stringsProvider, IGameService gameService, ICategoryService categoryService)
            : base(stringsProvider)
        {
            _gameService = gameService;
            _categoryService = categoryService;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Player Model
        {
            get => _model;
            set
            {
                if (!SetProperty(ref _model, value))
                    return;
                var _ = UpdateCategoryAsync();
            }
        }

        public Category Category
        {
            get => _category;
            set
            {
                if (!SetProperty(ref _category, value))
                    return;
                var _ = UpdatePlayerTileAsync();
            }
        }

        public bool MoveBackwards
        {
            get => _moveBackwards;
            set => SetProperty(ref _moveBackwards, value);
        }

        #endregion PROPERTIES


        #region METHODS

        private async Task UpdateCategoryAsync()
        {
            if (_isUpdatingModel)
                return;

            _isUpdatingCategory = true;

            var categoryId = _gameService
                .Game
                .GameBoard
                .First(tile => tile.Players.Any(player => player.Id == Model.Id))
                .CategoryId;

            Category = await _categoryService.GetOneAsync(categoryId);

            _isUpdatingCategory = false;
        }

        private async Task UpdatePlayerTileAsync()
        {
            if (_isUpdatingCategory)
                return;

            _isUpdatingModel = true;

            if (MoveBackwards)
                await _gameService.MovePlayerBackwardsToAsync(Model.Id, Category.Id);
            else
                await _gameService.MovePlayerToAsync(Model.Id, Category.Id);

            _isUpdatingModel = false;
        }

        #endregion METHODS
    }
}