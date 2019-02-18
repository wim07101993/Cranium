using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.Data.RestClient.Models.Bases;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;
using Prism.Commands;
using Shared.Extensions;

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

            UpdateCollectionAsync();
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

        #endregion PROPERTIES


        #region METHODS

        public async Task UpdateCollectionAsync()
        {
            _isUpdating = true;
            var newItems = await DataService.GetAsync<T>();
            ItemsSource.Clear();

            foreach (var newItem in newItems)
            {
                newItem.Save();
                ItemsSource.Add(newItem);
            }

            _isUpdating = false;
        }

        public void Create() => ItemsSource.Add(ConstructElement());

        protected virtual T ConstructElement() => Activator.CreateInstance<T>();

        public async Task SaveAsync()
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

            foreach (var item in ItemsSource.Where(x => ModifiedItems.Any(c => c == x.Id)))
            {
                await DataService.UpdateAsync(item);
                item.Save();
            }
        }

        public void Delete(T item) => ItemsSource.Remove(item);

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isUpdating)
                return;

            var newItems = e.NewItems?.Cast<IWithId>().Select(x => x.Id);
            var oldItems = e.OldItems?.Cast<IWithId>().Select(x => x.Id);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (newItems != null)
                        CreatedItems.AddRange(newItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (oldItems != null)
                        DeletedItems.AddRange(oldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (newItems != null)
                        CreatedItems.AddRange(newItems);
                    if (oldItems != null)
                        DeletedItems.AddRange(oldItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    if (oldItems != null)
                        DeletedItems.AddRange(oldItems);
                    break;
            }
        }

        #endregion METHODS
    }
}