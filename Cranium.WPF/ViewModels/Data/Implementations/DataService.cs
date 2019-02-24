using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Extensions;
using Cranium.WPF.Services.Mongo;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class DataService : IDataService
    {
        #region FIELDS

        private readonly IUnityContainer _unityContainer;

        private readonly ICategoryService _categoryService;
        private readonly IQuestionTypeService _questionTypeService;
        private readonly IQuestionService _questionService;

        private readonly ObservableCollection<ICategoryViewModel> _categoryViewModels =
            new ObservableCollection<ICategoryViewModel>();

        private readonly ObservableCollection<IQuestionTypeViewModel> _questionTypeViewModels =
            new ObservableCollection<IQuestionTypeViewModel>();

        private readonly ObservableCollection<IQuestionViewModel> _questionViewModels =
            new ObservableCollection<IQuestionViewModel>();

        private readonly object _updateCategoriesLock = new object();
        private readonly object _updateQuestionTypesLock = new object();
        private readonly object _updateQuestionsLock = new object();

        #endregion FIELDS


        #region CONSTRUCTORS

        public DataService(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;

            _categoryService = unityContainer.Resolve<ICategoryService>();
            _questionTypeService = unityContainer.Resolve<IQuestionTypeService>();
            _questionService = unityContainer.Resolve<IQuestionService>();
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public ObservableCollection<ICategoryViewModel> CategoryViewModels
        {
            get
            {
                lock (_updateCategoriesLock)
                    if (UpdateCategoriesTask == null || UpdateCategoriesTask.Status != TaskStatus.Running)
                        UpdateCategoriesTask = UpdateCategoriesAsync();

                return _categoryViewModels;
            }
        }

        public Task UpdateCategoriesTask { get; private set; }

        public ObservableCollection<IQuestionTypeViewModel> QuestionTypeViewModels
        {
            get
            {
                lock (_updateQuestionTypesLock)
                    if (UpdateQuestionTypesTask == null || UpdateQuestionTypesTask.Status != TaskStatus.Running)
                        UpdateQuestionTypesTask = UpdateQuestionTypesAsync();

                return _questionTypeViewModels;
            }
        }

        public Task UpdateQuestionTypesTask { get; private set; }

        public ObservableCollection<IQuestionViewModel> QuestionViewModels
        {
            get
            {
                lock (_updateQuestionsLock)
                    if (UpdateQuestionsTask == null || UpdateQuestionsTask.Status != TaskStatus.Running)
                        UpdateQuestionsTask = UpdateQuestionsAsync();

                return _questionViewModels;
            }
        }

        public Task UpdateQuestionsTask { get; private set; }

        #endregion PROPERTIES


        #region METHODS

        private async Task UpdateCategoriesAsync()
        {
            var categories = await _categoryService.GetAsync();

            for (var i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                var fi = CategoryViewModels.FindFirstIndex(x => x.Model.Id == category.Id);
                if (fi < 0)
                    break;

                CategoryViewModels[fi].Model = category;
                categories.RemoveAt(i);
                i--;
            }

            CategoryViewModels.Add(categories.Select(x =>
            {
                var vm = _unityContainer.Resolve<ICategoryViewModel>();
                vm.Model = x;
                return vm;
            }));
        }

        private async Task UpdateQuestionTypesAsync()
        {
            var questionTypes = await _questionTypeService.GetAsync();

            for (var i = 0; i < questionTypes.Count; i++)
            {
                var questionType = questionTypes[i];
                var fi = QuestionTypeViewModels.FindFirstIndex(x => x.Model.Id == questionType.Id);
                if (fi < 0)
                    break;

                QuestionTypeViewModels[fi].Model = questionType;
                questionTypes.RemoveAt(i);
                i--;
            }

            QuestionTypeViewModels.Add(questionTypes.Select(x =>
            {
                var vm = _unityContainer.Resolve<IQuestionTypeViewModel>();
                vm.Model = x;
                return vm;
            }));
        }

        private async Task UpdateQuestionsAsync()
        {
            var questions = await _questionService.GetAsync();

            for (var i = 0; i < questions.Count; i++)
            {
                var question = questions[i];
                var fi = QuestionViewModels.FindFirstIndex(x => x.Model.Id == question.Id);
                if (fi < 0)
                    break;

                QuestionViewModels[fi].Model = question;
                questions.RemoveAt(i);
                i--;
            }

            QuestionViewModels.Add(questions.Select(x =>
            {
                var vm = _unityContainer.Resolve<IQuestionViewModel>();
                vm.Model = x;
                return vm;
            }));
        }
        
        #endregion METHODS
    }
}