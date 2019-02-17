using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Models;

namespace Cranium.WPF.ViewModels
{
    public interface IDataViewModel : IViewModelBase
    {
        ObservableCollection<Question> Questions { get; }
        ObservableCollection<QuestionType> QuestionTypes { get; }
        ObservableCollection<Category> Categories { get; }
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
