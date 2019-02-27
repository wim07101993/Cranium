using System.Windows.Input;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IAnswerViewModel : IModelContainer<Answer>
    {
        ICommand PickFileCommand { get; }
    }
}
