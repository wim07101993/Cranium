using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IQuestionViewModel : IModelContainer<Question>
    {
        byte[] Attachment { get; }
        BitmapImage ImageSource { get; }

        ICommand ChangeAttachmentCommand { get; }

        IAnswersViewModel AnswersViewModel { get; }


        Task ChangeAttachmentAsync();
    }
}