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

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataGridViewModel<T> : AViewModelBase, IDataGridViewModel<T> where T : AWithId
    {
        #region FIELDS

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

        #endregion PROPERTIES


        #region METHODS

        public void Create() => ItemsSource.Add(ConstructElement());

        protected virtual T ConstructElement() => Activator.CreateInstance<T>();

        public async Task SaveAsync()
        {
            foreach (var item in ItemsSource.Where(x => CreatedItems.Any(c => c == x.Id)))
                await DataService.CreateAsync(item);

            foreach (var guid in DeletedItems)
                await DataService.DeleteAsync<T>(guid);

            foreach (var item in ItemsSource.Where(x => ModifiedItems.Any(c => c == x.Id)))
                await DataService.UpdateAsync(item);
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
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