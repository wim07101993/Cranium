using System.Collections.ObjectModel;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface IQuestionTypesViewModel : ICollectionViewModel<IQuestionTypeViewModel>
    {
        ObservableCollection<Category> Categories { get; }
    }
}