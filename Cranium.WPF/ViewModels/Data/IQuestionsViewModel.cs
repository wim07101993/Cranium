using System.Collections.ObjectModel;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IQuestionsViewModel : ICollectionViewModel<IQuestionViewModel>
    {
        ObservableCollection<QuestionType> QuestionTypes { get; }
    }
}