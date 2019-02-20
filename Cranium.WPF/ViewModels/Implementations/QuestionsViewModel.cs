using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class QuestionsViewModel : DataGridViewModel<Question>, IQuestionsViewModel
    {
        private readonly IQuestionTypesViewModel _questionTypesViewModel;


        public QuestionsViewModel(IStringsProvider stringsProvider, IClient dataService, IQuestionTypesViewModel questionTypesViewModel)
            : base(stringsProvider, dataService)
        {
            _questionTypesViewModel = questionTypesViewModel;
        }

        public ObservableCollection<QuestionType> QuestionTypes => _questionTypesViewModel.ItemsSource;
    }
}