using System.Collections.ObjectModel;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IAnswersViewModel : ICollectionViewModel<IAnswerViewModel>
    {
        ObservableCollection<Answer> Models { get; set; }
    }
}
