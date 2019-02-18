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
    public abstract class ADataGridViewModel<T> : AViewModelBase, IDataGridViewModel<T> where T : AWithId
    {
        #region FIELDS


        #endregion FIELDS


        #region CONSTRUCTOR

        public ADataGridViewModel(IStringsProvider stringsProvider, IClient dataService) 
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

        public void Create()
        {
            ItemsSource.Add(ConstructElement());
        }

        protected abstract T ConstructElement();

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
                    CreatedItems.AddRange(newItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    DeletedItems.AddRange(oldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    CreatedItems.AddRange(newItems);
                    DeletedItems.AddRange(oldItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    DeletedItems.AddRange(oldItems);
                    break;
            }
        }

        #endregion METHODS
    }
}
