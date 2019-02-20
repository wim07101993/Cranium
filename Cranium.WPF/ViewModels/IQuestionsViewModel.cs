using Cranium.Data.RestClient.Models;
using System.Collections.ObjectModel;

namespace Cranium.WPF.ViewModels
{
    public interface IQuestionsViewModel : IDataGridViewModel<Question>
    {
        ObservableCollection<QuestionType> QuestionTypes { get; }
    }
}