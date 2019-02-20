using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;
using Prism.Commands;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class QuestionsViewModel : DataGridViewModel<Question>, IQuestionsViewModel
    {
        private readonly IQuestionTypesViewModel _questionTypesViewModel;


        public QuestionsViewModel(IStringsProvider stringsProvider, IClient dataService, IQuestionTypesViewModel questionTypesViewModel)
            : base(stringsProvider, dataService)
        {
            _questionTypesViewModel = questionTypesViewModel;
            _questionTypesViewModel.ItemsSource.CollectionChanged += OnCategoriesChanged;

            AddAnswerCommand = new DelegateCommand<Question>(AddAnswer);
        }


        public ObservableCollection<QuestionType> QuestionTypes => _questionTypesViewModel.ItemsSource;

        public ICommand AddAnswerCommand { get; }


        public void AddAnswer(Question question) => question.Answers.Add(new Answer());

        protected override async Task OnReceivedItemAsync(Question item)
        {
            if (_questionTypesViewModel.ItemsSource.Count == 0)
            {
                if (_questionTypesViewModel.UpdateTask?.Status == TaskStatus.Running)
                    _questionTypesViewModel.UpdateTask.Wait();
                else
                    await _questionTypesViewModel.UpdateCollectionAsync();
            }

            item.QuestionType = _questionTypesViewModel.ItemsSource.FirstOrDefault(x => x.Id == item.QuestionTypeId);
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems?.Cast<QuestionType>().ToList();
            var oldItems = e.OldItems?.Cast<QuestionType>().ToList();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var questionType in ItemsSource)
                        if (questionType.QuestionType == null && questionType.QuestionTypeId != default)
                            questionType.QuestionType = newItems?.FirstOrDefault(x => x.Id == questionType.QuestionTypeId);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (oldItems != null)
                    {
                        foreach (var category in oldItems)
                            foreach (var questionType in ItemsSource.Where(x => x.QuestionTypeId == category.Id))
                            {
                                questionType.QuestionType = null;
                                questionType.QuestionTypeId = default;
                            }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (var questionType in ItemsSource)
                    {
                        questionType.QuestionType = null;
                        questionType.QuestionTypeId = default;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}