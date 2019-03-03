using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Game
{
    public interface IQuestionViewModel : IViewModelBase
    {
        Question Question { get; }
        BitmapImage ImageAttachment { get; }

        ICommand AnswerCommand { get; }

        bool HasAnswered { get; }
        bool IsAnswerCorrect { get; }

        IEnumerable<Answer> CorrectAnswers { get; }

        ICommand SelectCategoryCommand { get; }
        IEnumerable<Category> Categories { get; }


        Task Answer(bool correct);
    }
}
