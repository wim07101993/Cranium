using System.Collections.ObjectModel;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class QuestionTypesViewModel : DataGridViewModel<QuestionType>, IQuestionTypesViewModel
    {
        private readonly ICategoriesViewModel _categoriesViewModel;


        public QuestionTypesViewModel(IStringsProvider stringsProvider, IClient dataService, ICategoriesViewModel categoriesViewModel)
            : base(stringsProvider, dataService)
        {
            _categoriesViewModel = categoriesViewModel;
            _categoriesViewModel.ItemsSource.CollectionChanged +=
                (sender, args) => RaisePropertyChanged(nameof(Categories));
        }


        public ObservableCollection<Category> Categories => _categoriesViewModel.ItemsSource;
    }
}