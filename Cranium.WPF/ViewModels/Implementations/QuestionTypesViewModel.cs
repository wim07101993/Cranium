using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class QuestionTypesViewModel : DataGridViewModel<QuestionType>, IQuestionTypesViewModel
    {
        private readonly ICategoriesViewModel _categoriesViewModel;


        public QuestionTypesViewModel(
            IStringsProvider stringsProvider, IClient dataService, ICategoriesViewModel categoriesViewModel)
            : base(stringsProvider, dataService)
        {
            _categoriesViewModel = categoriesViewModel;
            _categoriesViewModel.ItemsSource.CollectionChanged += OnCategoriesChanged;
        }


        public ObservableCollection<Category> Categories => _categoriesViewModel.ItemsSource;


        protected override async Task OnReceivedItemAsync(QuestionType item)
        {
            if (_categoriesViewModel.ItemsSource.Count == 0)
            {
                if (_categoriesViewModel.UpdateTask?.Status == TaskStatus.Running)
                    _categoriesViewModel.UpdateTask.Wait();
                else
                    await _categoriesViewModel.UpdateCollectionAsync();
            }

            item.Category = _categoriesViewModel.ItemsSource.FirstOrDefault(x => x.Id == item.CategoryId);
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems?.Cast<Category>().ToList();
            var oldItems = e.OldItems?.Cast<Category>().ToList();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var questionType in ItemsSource)
                        if (questionType.Category == null && questionType.CategoryId != default)
                            questionType.Category = newItems?.FirstOrDefault(x => x.Id == questionType.CategoryId);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (oldItems != null)
                    {
                        foreach (var category in oldItems)
                        foreach (var questionType in ItemsSource.Where(x => x.CategoryId == category.Id))
                        {
                            questionType.Category = null;
                            questionType.CategoryId = default;
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (var questionType in ItemsSource)
                    {
                        questionType.Category = null;
                        questionType.CategoryId = default;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}