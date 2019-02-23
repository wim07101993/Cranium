using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Events;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.ViewModels.Implementations;
using Prism.Events;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionTypesViewModel : ACollectionViewModel<QuestionType, IQuestionTypeViewModel>, IQuestionTypesViewModel
    {
        private readonly ICategoryService _categoryService;


        public QuestionTypesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _categoryService = unityContainer.Resolve<ICategoryService>();
            unityContainer.Resolve<IEventAggregator>()
                .GetEvent<CategoryChangedEvent>()
                .Subscribe(async x => await UpdateCategoryAsync(x));
        }


        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();


        public override async Task UpdateCollectionAsync()
        {
            await base.UpdateCollectionAsync();

            var categories = await _categoryService.GetAsync();
            Categories.Clear();
            Categories.Add(categories);
        }

        private async Task UpdateCategoryAsync(Category category)
        {
            var i = Categories.FindFirstIndex(x => category.Id == x.Id);
            Categories[i].Color = category.Color;
            Categories[i].Description = category.Description;
            Categories[i].Image = category.Image;
            Categories[i].Name = category.Name;
            Categories[i].Id = category.Id;
        }
    }
}