using Cranium.WPF.ViewModels.Data;

namespace Cranium.WPF.ViewModels
{
    public interface IDataViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        IQuestionsViewModel QuestionsViewModel { get; }
        IQuestionTypesViewModel QuestionTypesViewModel { get; }
        ICategoriesViewModel CategoriesViewModel { get; }
    }
}
