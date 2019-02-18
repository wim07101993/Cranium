using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataViewModel : AViewModelBase, IDataViewModel
    {
        #region FIELDS
        
        #endregion FIELDS


        #region CONSTRUCTOR

        public DataViewModel(
            IStringsProvider stringsProvider,
            IHamburgerMenuViewModel hamburgerMenuViewModel, IQuestionsViewModel questionsViewModel,
            IQuestionTypesViewModel questionTypesViewModel, ICategoriesViewModel categoriesViewModel)
            : base(stringsProvider)
        {
           HamburgerMenuViewModel = hamburgerMenuViewModel;
            QuestionsViewModel = questionsViewModel;
            QuestionTypesViewModel = questionTypesViewModel;
            CategoriesViewModel = categoriesViewModel;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES
        
        public IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public IQuestionsViewModel QuestionsViewModel { get; }
        public IQuestionTypesViewModel QuestionTypesViewModel { get; }
        public ICategoriesViewModel CategoriesViewModel { get; }

        #endregion PROPERTIES


        #region METHODS
        
        #endregion METHODS
    }
}