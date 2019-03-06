using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Game.Player
{
    public class PlayerViewModel : AModelContainerViewModel<Player>
    {
        #region FIELDS

        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        private Category _category;
        private bool _isUpdatingCategory;
        private bool _isUpdatingModel;
        private bool _moveBackward;

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

        public bool MoveBackward
        {
            get => _moveBackward;
            set => SetProperty(ref _moveBackward, value);
        }

        #endregion PROPERTIES


        #region METHODS

        protected override async Task OnModelChangedAsync()
        {
            await UpdateCategoryAsync();
        }

        private async Task UpdateCategoryAsync()
        {
            if (_isUpdatingModel)
                return;

            _isUpdatingCategory = true;

            var gameBoard = _gameService.GameBoard;
            if (gameBoard != null)
            { 
                var categoryId = _gameService
                    .GameBoard
                    .First(tile => tile.Players.Any(player => player.Id == Model.Id))
                    .CategoryId;
            
                Category = await _categoryService.GetOneAsync(categoryId);
            }

            _isUpdatingCategory = false;
        }

        private async Task UpdatePlayerTileAsync()
        {
            if (_isUpdatingCategory)
                return;

            _isUpdatingModel = true;

            if (MoveBackward)
                await _gameService.MovePlayerBackwardsToAsync(Model.Id, Category.Id);
            else
                await _gameService.MovePlayerToAsync(Model.Id, Category.Id);

            Category = null;

            _isUpdatingModel = false;
        }

        #endregion METHODS
    }
}