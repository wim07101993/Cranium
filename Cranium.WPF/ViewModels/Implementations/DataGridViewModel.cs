using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.Data.RestClient.Models.Bases;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;
using Prism.Commands;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataGridViewModel<T> : AViewModelBase, IDataGridViewModel<T> where T : AWithId
    {
        #region FIELDS

        private bool _isUpdating;

        #endregion FIELDS


        #region CONSTRUCTOR

        protected DataGridViewModel(IStringsProvider stringsProvider, IClient dataService)
            : base(stringsProvider)
        {
            DataService = dataService;

            ItemsSource = new ObservableCollection<T>();
            ItemsSource.CollectionChanged += OnItemsCollectionChanged;

            CreateCommand = new DelegateCommand(Create);
            SaveCommand = new DelegateCommand(async () => await SaveAsync());
            DeleteCommand = new DelegateCommand<T>(Delete);
            UpdateCommand = new DelegateCommand(async () => await UpdateCollectionAsync());

            UpdateTask = UpdateCollectionAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        protected IClient DataService { get; }

        public ObservableCollection<T> ItemsSource { get; }

        protected List<Guid> CreatedItems { get; } = new List<Guid>();
        protected List<Guid> DeletedItems { get; } = new List<Guid>();

        protected List<Guid> ModifiedItems
            => ItemsSource
                ?.Where(x => x.ChangedProperties?.Any() == true)
                .Select(x => x.Id)
                .ToList();

        public ICommand CreateCommand { get; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }

        public Task UpdateTask { get; protected set; }

        #endregion PROPERTIES


        #region METHODS

        public async Task UpdateCollectionAsync()
        {
            _isUpdating = true;

            try
            {
                var newItems = await DataService.GetAsync<T>();

                ItemsSource.Clear();

                foreach (var newItem in newItems)
                {
                    await OnReceivedItemAsync(newItem);
                    newItem.Save();
                    ItemsSource.Add(newItem);
                }
            }
            catch (Exception e)
            {
                // todo
            }

            _isUpdating = false;
        }

        protected virtual Task OnReceivedItemAsync(T item)
        {
            return Task.CompletedTask;
        }

        public void Create() => ItemsSource.Add(ConstructElement());

        protected virtual T ConstructElement() => Activator.CreateInstance<T>();

        public async Task SaveAsync()
        {
            try
            {
                foreach (var item in ItemsSource.Where(x => CreatedItems.Any(c => c == x.Id)))
                {
                    var newItem = await DataService.CreateAsync(item);
                    item.Id = newItem.Id;
                    item.Save();
                }

                CreatedItems.Clear();

                foreach (var guid in DeletedItems)
                    await DataService.DeleteAsync<T>(guid);
                DeletedItems.Clear();

                if (UpdateTask.Status == TaskStatus.Running)
                    UpdateTask.Wait();

                foreach (var item in ItemsSource.Where(x => ModifiedItems.Any(c => c == x.Id)))
                {
                    UpdateTask = DataService.UpdateAsync(item);
                    await UpdateTask;
                    item.Save();
                }
            }
            catch (Exception e)
            {
                // todo
            }
        }

        public void Delete(T item) => ItemsSource.Remove(item);

        protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems?.Cast<IWithId>();
            var oldItems = e.OldItems?.Cast<IWithId>();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (newItems != null)
                        foreach (var item in newItems)
                        {
                            item.PropertyChanged += OnItemPropertyChanged;
                            if (!_isUpdating)
                                CreatedItems.Add(item.Id);
                        }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (oldItems != null)
                        foreach (var item in oldItems)
                        {
                            item.PropertyChanged -= OnItemPropertyChanged;
                            if (!_isUpdating)
                                DeletedItems.Add(item.Id);
                        }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (newItems != null)
                        foreach (var item in newItems)
                        {
                            item.PropertyChanged += OnItemPropertyChanged;
                            if (!_isUpdating)
                                CreatedItems.Add(item.Id);
                        }

                    if (oldItems != null)
                        foreach (var item in oldItems)
                        {
                            item.PropertyChanged -= OnItemPropertyChanged;
                            if (!_isUpdating)
                                DeletedItems.Add(item.Id);
                        }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    if (oldItems != null)
                        foreach (var item in oldItems)
                        {
                            item.PropertyChanged -= OnItemPropertyChanged;
                            if (!_isUpdating)
                                DeletedItems.Add(item.Id);
                        }

                    break;
            }
        }

        protected virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion METHODS
    }
}