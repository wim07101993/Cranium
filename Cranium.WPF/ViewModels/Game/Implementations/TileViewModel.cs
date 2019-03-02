using System.Threading.Tasks;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class TileViewModel : AViewModelBase, ITileViewModel
    {
        private readonly ICategoryService _categoryService;

        #region FIELDS

        private Tile _model;
        private Category _category;

        #endregion FIELDS


        #region CONSTRUCTORS

        public TileViewModel(IStringsProvider stringsProvider, ICategoryService categoryService) : base(stringsProvider)
        {
            _categoryService = categoryService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Tile Model
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
            private set => SetProperty(ref _category, value);
        }

        #endregion PROPERTIES


        #region METHODS

        private async Task UpdateCategoryAsync()
        {
            if (Model == null)
            {
                Category = null;
                return;
            }

            Category = await _categoryService.GetOneAsync(Model.CategoryId);
        }

        #endregion METHODS
    }
}