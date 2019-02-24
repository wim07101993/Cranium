using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IDataService
    {
        ObservableCollection<ICategoryViewModel> CategoryViewModels { get; }
        Task UpdateCategoriesTask { get; }

        ObservableCollection<IQuestionTypeViewModel> QuestionTypeViewModels { get; }
        Task UpdateQuestionTypesTask { get; }

        ObservableCollection<IQuestionViewModel> QuestionViewModels { get; }
        Task UpdateQuestionsTask { get; }
    }
}
