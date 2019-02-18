using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataViewModel : AViewModelBase, IDataViewModel
    {
        #region FIELDS

        private readonly IClient _dataService;

        #endregion FIELDS


        #region CONSTRUCTOR

        public DataViewModel(IStringsProvider stringsProvider, IHamburgerMenuViewModel hamburgerMenuViewModel,
            IClient dataService) 
            : base(stringsProvider)
        {
            _dataService = dataService;
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            FetchDataAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public ObservableCollection<Question> Questions { get; } = new ObservableCollection<Question>();
        public ObservableCollection<QuestionType> QuestionTypes { get; } = new ObservableCollection<QuestionType>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task FetchDataAsync()
        {
            Questions.Clear();
            foreach (var question in await _dataService.GetAsync<Question>())
                Questions.Add(question);

            QuestionTypes.Clear();
            foreach (var questionType in await _dataService.GetAsync<QuestionType>())
                QuestionTypes.Add(questionType);

            Categories.Clear();
            foreach (var category in await _dataService.GetAsync<Category>())
                Categories.Add(category);
        }

        #endregion METHODS
    }
}
