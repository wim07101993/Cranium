using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class QuestionTypesViewModel : DataGridViewModel<QuestionType>, IQuestionTypesViewModel
    {
        public QuestionTypesViewModel(IStringsProvider stringsProvider, IClient dataService)
            : base(stringsProvider, dataService)
        {
        }
    }
}