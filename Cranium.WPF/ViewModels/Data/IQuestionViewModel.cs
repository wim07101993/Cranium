using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IQuestionViewModel : IModelContainer<Question>
    {
        byte[] Attachment { get; }
        ICommand ChangeAttachmentCommand { get; }

        Task ChangeAttachmentAsync();
    }
}
