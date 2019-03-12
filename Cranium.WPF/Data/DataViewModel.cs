using Cranium.WPF.Data.Category;
using Cranium.WPF.Data.Question;
using Cranium.WPF.Data.QuestionType;
using Cranium.WPF.HamburgerMenu;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.Data
{
    public class DataViewModel : AViewModelBase
    {
        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTOR

        public DataViewModel(
            IStringsProvider stringsProvider,
            HamburgerMenuViewModel hamburgerMenuViewModel, QuestionsViewModel questionsViewModel,
            QuestionTypesViewModel questionTypesViewModel, CategoriesViewModel categoriesViewModel)
            : base(stringsProvider)
        {
            HamburgerMenuViewModel = hamburgerMenuViewModel;
            QuestionsViewModel = questionsViewModel;
            QuestionTypesViewModel = questionTypesViewModel;
            CategoriesViewModel = categoriesViewModel;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public HamburgerMenuViewModel HamburgerMenuViewModel { get; }
        public QuestionsViewModel QuestionsViewModel { get; }
        public QuestionTypesViewModel QuestionTypesViewModel { get; }
        public CategoriesViewModel CategoriesViewModel { get; }

        #endregion PROPERTIES


        #region METHODS

        #endregion METHODS
    }
}