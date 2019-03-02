using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IQuestionViewModel : IModelContainer<Question>
    {
        byte[] Attachment { get; }
        ImageSource ImageSource { get; }

        ICommand ChangeAttachmentCommand { get; }

        IAnswersViewModel AnswersViewModel { get; }


        Task ChangeAttachmentAsync();
    }
}