using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.ViewModels.Implementations;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionTypesViewModel : ACollectionViewModel<QuestionType, IQuestionTypeViewModel>, IQuestionTypesViewModel
    {
        private readonly ICategoryService _categoryService;


        public QuestionTypesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _categoryService = unityContainer.Resolve<ICategoryService>();
        }


        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();


        public override async Task UpdateCollectionAsync()
        {
            var categories = await _categoryService.GetAsync();
            Categories.Clear();
            Categories.Add(categories);

            await base.UpdateCollectionAsync();
        }
    }
}