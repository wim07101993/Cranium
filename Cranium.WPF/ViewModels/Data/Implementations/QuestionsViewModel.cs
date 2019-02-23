using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.ViewModels.Implementations;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class QuestionsViewModel : ACollectionViewModel<Question, IQuestionViewModel>, IQuestionsViewModel
    {
        private readonly IQuestionTypeService _questionTypeService;


        public QuestionsViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            _questionTypeService = unityContainer.Resolve<IQuestionTypeService>();
        }


        public ObservableCollection<QuestionType> QuestionTypes {get;} = new ObservableCollection<QuestionType>();


        public override async Task UpdateCollectionAsync()
        {
            var questionTypes = await _questionTypeService.GetAsync();
            QuestionTypes.Clear();
            QuestionTypes.Add(questionTypes);

            await base.UpdateCollectionAsync();
        }
    }
}