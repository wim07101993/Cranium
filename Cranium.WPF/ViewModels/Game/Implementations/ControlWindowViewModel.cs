using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Game;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.ViewModels.Implementations;
using Unity;

namespace Cranium.WPF.ViewModels.Game.Implementations
{
    public class ControlWindowViewModel : ACollectionViewModel<Player, IPlayerViewModel>, IControlWindowViewModel
    {
        #region FIELDS

        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        private List<Category> _categories;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ControlWindowViewModel(IUnityContainer unityContainer)
            : base(unityContainer)
        {
            _gameService = unityContainer.Resolve<IGameService>();
            _categoryService = unityContainer.Resolve<ICategoryService>();
            _gameService.GameChangedEvent += OnGameChanged;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public List<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        #endregion PROPERTIES


        #region METHODS

        private void OnGameChanged(object sender, System.EventArgs e)
        {
            var _ = OnGameChangedAsync();
        }

        private async Task OnGameChangedAsync()
        {
            var questions = _gameService.Game.Questions;
            var categories = questions
                .Select(x => x.QuestionType.Category)
                .Distinct(new CategoryIdComparer())
                .ToList();

            var specialCategory = await _categoryService.GetByAsync(x => x.IsSpecial);
            categories.Add(specialCategory);
            Categories = categories;
        }

        #endregion METHODS


        #region CLASSES

        private class CategoryIdComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y) => x != null && x.Id == y?.Id;

            public int GetHashCode(Category category) => category.Id.GetHashCode();
        }

        #endregion CLASSES
    }
}