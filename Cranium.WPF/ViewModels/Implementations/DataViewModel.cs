using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Models;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataViewModel : AViewModelBase, IDataViewModel
    {
        public DataViewModel(IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel) 
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
        }


        public ObservableCollection<Question> Questions { get; } = new ObservableCollection<Question>();
        public ObservableCollection<QuestionType> QuestionTypes { get; } = new ObservableCollection<QuestionType>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
    }
}
