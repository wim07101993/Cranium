namespace Cranium.WPF.ViewModels.Data
{
    public interface IDataViewModel : IViewModelBase
    {
        IHamburgerMenuViewModel HamburgerMenuViewModel { get; }
        IQuestionsViewModel QuestionsViewModel { get; }
        IQuestionTypesViewModel QuestionTypesViewModel { get; }
        ICategoriesViewModel CategoriesViewModel { get; }
    }
}
