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
        private readonly ICategoryService _categoryService;


        public QuestionTypesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _categoryService = unityContainer.Resolve<ICategoryService>();
            unityContainer.Resolve<IEventAggregator>()
                .GetEvent<CategoryChangedEvent>()
                .Subscribe(UpdateCategory);

            var _ = UpdateCollectionAsync();
        }


        public ObservableCollection<Category.Category> Categories { get; } = new ObservableCollection<Category.Category>();


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
        }
    }
}