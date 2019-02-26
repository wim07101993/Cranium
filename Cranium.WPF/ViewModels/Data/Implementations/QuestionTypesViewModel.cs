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
    public sealed class QuestionTypesViewModel : ACollectionViewModel<QuestionType, IQuestionTypeViewModel>, IQuestionTypesViewModel
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


        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();


        public override async Task UpdateCollectionAsync()
        {
            var categories = await _categoryService.GetAsync();
            Categories.Clear();
            Categories.Add(categories);

            await base.UpdateCollectionAsync();
        }

        private void UpdateCategory(Category category)
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