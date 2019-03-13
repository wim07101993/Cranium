using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Prism.Events;
using Unity;

namespace Cranium.WPF.Data.QuestionType
{
    public sealed class QuestionTypesViewModel : ACollectionViewModel<QuestionType, QuestionTypeViewModel>
    {
        #region FIELDS

        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;

        #endregion FIELDS


        #region CONSTRUCTOR

        public QuestionTypesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _categoryService = unityContainer.Resolve<ICategoryService>();
            _eventAggregator = unityContainer.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<CategoryChangedEvent>().Subscribe(UpdateCategory);

            var _ = UpdateCollectionAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ObservableCollection<Category.Category> Categories { get; } = new ObservableCollection<Category.Category>();

        #endregion PROPERTIES


        #region METHODS

        public override async Task UpdateCollectionAsync()
        {
            var categories = await _categoryService.GetAsync();
            Categories.Clear();
            Categories.Add(categories);

            await base.UpdateCollectionAsync();
        }

        private void UpdateCategory(Category.Category category)
        {
            var i = Categories.FindFirstIndex(x => category.Id == x.Id);
            if (i < 0)
                return;

            Categories[i].Color = category.Color;
            Categories[i].Description = category.Description;
            Categories[i].Image = category.Image;
            Categories[i].Name = category.Name;
            Categories[i].Id = category.Id;

            foreach (var viewModel in ItemsSource)
            {
                if (viewModel.Model.Category.Id == category.Id)
                {
                    viewModel.Model.Category.Color = category.Color;
                    viewModel.Model.Category.Description = category.Description;
                    viewModel.Model.Category.Image = category.Image;
                    viewModel.Model.Category.Name = category.Name;
                    viewModel.Model.Category.Id = category.Id;
                }
            }
        }

        public override async Task SaveAsync(QuestionTypeViewModel viewModel)
        {
            await base.SaveAsync(viewModel);
            _eventAggregator.GetEvent<QuestionTypeChangedEvent>().Publish(viewModel.Model);
        }

        #endregion METHODS
    }
}