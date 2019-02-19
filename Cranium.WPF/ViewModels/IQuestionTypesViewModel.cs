using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Models;

namespace Cranium.WPF.ViewModels
{
    public interface IQuestionTypesViewModel : IDataGridViewModel<QuestionType>
    {
        ObservableCollection<Category> Categories { get; }
    }
}