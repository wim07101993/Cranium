using Cranium.Data.RestClient.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cranium.WPF.ViewModels
{
    public interface IQuestionsViewModel : IDataGridViewModel<Question>
    {
        ObservableCollection<QuestionType> QuestionTypes { get; }

        ICommand PickFileCommand { get; }
    }
}