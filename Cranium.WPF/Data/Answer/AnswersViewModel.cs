using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using MongoDB.Bson;
using Prism.Commands;
using Shared.Extensions;
using Unity;

namespace Cranium.WPF.Data.Answer
{
    public class AnswersViewModel : AViewModelBase
    {
        #region FIELDS

        private readonly IUnityContainer _unityContainer;
        private readonly ObservableCollection<Answer> _models;

        #endregion FIELDS


        #region CONSTRUCTORS

        public AnswersViewModel(IUnityContainer unityContainer) : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;

            _models = new ObservableCollection<Answer>();
            _models.CollectionChanged += OnModelsCollectionChanged;

            CreateCommand = new DelegateCommand(async () => await CreateAsync());
            DeleteCommand = new DelegateCommand<AnswerViewModel>(async x => await DeleteAsync(x));
            UpdateCollectionCommand = new DelegateCommand(async () => await UpdateCollectionAsync());
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public ObservableCollection<AnswerViewModel> ItemsSource { get; } =
            new ObservableCollection<AnswerViewModel>();

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCollectionCommand { get; }

        public ObservableCollection<Answer> Models
        {
            get => _models;
            set
            {
                if (Equals(_models, value))
                    return;

                foreach (var answer in _models)
                    answer.PropertyChanged -= OnAnswerPropertyChanged;
                _models.Clear();

                if (value != null && value.Count > 0)
                {
                    _models.Add(value);
                    foreach (var answer in _models)
                        answer.PropertyChanged += OnAnswerPropertyChanged;
                }
            }
        }

        #endregion PROPERTIES


        #region METHODS

        public Task CreateAsync()
        {
            Models.Add(new Answer
            {
                Id = ObjectId.GenerateNewId(),
                Value = "Answer"
            });

            return Task.CompletedTask;
        }

        public Task DeleteAsync(AnswerViewModel viewModel)
        {
            Models.Remove(viewModel.Model);
            return Task.CompletedTask;
        }

        public Task UpdateCollectionAsync()
        {
            ItemsSource.Clear();

            if (Models != null)
                ItemsSource.Add(Models.Select(x =>
                {
                    var vm = _unityContainer.Resolve<AnswerViewModel>();
                    vm.Model = x;
                    return vm;
                }));

            return Task.CompletedTask;
        }

        private void OnModelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var addedItems = e.NewItems?.Cast<Answer>().ToList();
            var removedItems = e.OldItems?.Cast<Answer>().ToList();

            if (addedItems != null)
                foreach (var addedItem in addedItems)
                    addedItem.PropertyChanged += OnAnswerPropertyChanged;
            if (removedItems != null)
                foreach (var removedItem in removedItems)
                    removedItem.PropertyChanged -= OnAnswerPropertyChanged;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if (addedItems == null)
                        break;

                    ItemsSource.Add(addedItems.Select(x =>
                    {
                        var vm = _unityContainer.Resolve<AnswerViewModel>();
                        vm.Model = x;
                        return vm;
                    }));
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    if (removedItems == null)
                        break;

                    ItemsSource.RemoveWhere(x => removedItems.Any(r => r.Id == x.Model.Id));
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    if (removedItems != null)
                        ItemsSource.RemoveWhere(x => removedItems.Any(r => r.Id == x.Model.Id));

                    if (addedItems != null)
                        ItemsSource.Add(addedItems.Select(x =>
                        {
                            var vm = _unityContainer.Resolve<AnswerViewModel>();
                            vm.Model = x;
                            return vm;
                        }));
                    break;
                }
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ItemsSource.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AnyAnswerChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAnswerPropertyChanged(object sender, PropertyChangedEventArgs e)
            => AnyAnswerChanged?.Invoke(this, EventArgs.Empty);

        #endregion METHODS


        #region EVENTS

        public event EventHandler AnyAnswerChanged;

        #endregion EVENTS
    }
}