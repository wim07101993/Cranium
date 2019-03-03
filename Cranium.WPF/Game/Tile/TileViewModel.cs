using System.Threading.Tasks;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Game.Tile
{
    public class TileViewModel : AModelContainerViewModel<Tile>
    {
        private readonly ICategoryService _categoryService;

        #region FIELDS

        private Category _category;

        #endregion FIELDS


        #region CONSTRUCTORS

        public TileViewModel(IStringsProvider stringsProvider, ICategoryService categoryService) : base(stringsProvider)
        {
            _categoryService = categoryService;
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES
        
        public Category Category
        {
            get => _category;
            private set => SetProperty(ref _category, value);
        }

        #endregion PROPERTIES


        #region METHODS

        protected override async Task OnModelChangedAsync()
        {
            await UpdateCategoryAsync();
        }

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