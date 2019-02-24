using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Prism.Commands;
using Shared.Extensions;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class AnswersViewModel : AViewModelBase, IAnswersViewModel
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
            DeleteCommand = new DelegateCommand<IAnswerViewModel>(async x => await DeleteAsync(x));
            UpdateCollectionCommand = new DelegateCommand(async () => await UpdateCollectionAsync());
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public ObservableCollection<IAnswerViewModel> ItemsSource { get; } =
            new ObservableCollection<IAnswerViewModel>();

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCollectionCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public Task CreateAsync()
        {
            Models.Add(new Answer {Value = "Answer"});
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IAnswerViewModel viewModel)
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
                    var vm = _unityContainer.Resolve<IAnswerViewModel>();
                    vm.Model = x;
                    return vm;
                }));

            return Task.CompletedTask;
        }

        public ObservableCollection<Answer> Models
        {
            get => _models;
            set
            {
                if (Equals(_models, value))
                    return;

                _models.Clear();
                _models.Add(value);
            }
        }


        private void OnModelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var addedItems = e.NewItems?.Cast<Answer>().ToList();
            var removedItems = e.OldItems?.Cast<Answer>().ToList();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if (addedItems == null)
                        break;

                    ItemsSource.Add(addedItems.Select(x =>
                    {
                        var vm = _unityContainer.Resolve<IAnswerViewModel>();
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
                            var vm = _unityContainer.Resolve<IAnswerViewModel>();
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
        }

        #endregion METHODS
    }
}