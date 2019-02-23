using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Models.Bases;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Prism.Commands;
using Unity;

namespace Cranium.WPF.ViewModels.Implementations
{
    public abstract class ACollectionViewModel<TModel, TViewModel> : AViewModelBase, ICollectionViewModel<TViewModel>
        where TModel : AWithId
        where TViewModel : IModelContainer<TModel>
    {
        #region FIELDS

        private readonly IUnityContainer _unityContainer;

        #endregion FIELDS


        #region CONSTRUCTOR

        protected ACollectionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            DataService = unityContainer.Resolve<IDataService<TModel>>();

            ItemsSource = new ObservableCollection<TViewModel>();

            CreateCommand = new DelegateCommand(async () => await CreateAsync());
            DeleteCommand = new DelegateCommand<TViewModel>(async x => await DeleteAsync(x));
            UpdateCollectionCommand = new DelegateCommand(async () => await UpdateCollectionAsync());

            UpdateCollectionAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        protected IDataService<TModel> DataService { get; }

        public ObservableCollection<TViewModel> ItemsSource { get; }

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCollectionCommand { get; }

        public Task UpdateTask { get; protected set; }

        #endregion PROPERTIES


        #region METHODS

        public virtual async Task UpdateCollectionAsync()
        {
            try
            {
                var newItems = await DataService.GetAsync();

                ItemsSource.Clear();
                foreach (var model in newItems)
                    AddModelToCollection(model);
            }
            catch (Exception e)
            {
                // todo
            }
        }

        public async Task CreateAsync()
        {
            var model = await DataService.CreateAsync(ConstructElement());
            AddModelToCollection(model);
        }

        protected virtual TModel ConstructElement() => Activator.CreateInstance<TModel>();

        public async Task DeleteAsync(TViewModel viewModel) => await DataService.RemoveAsync(viewModel.Model.Id);

        protected virtual void AddModelToCollection(TModel model)
        {
            var viewModel = _unityContainer.Resolve<TViewModel>();
            viewModel.Model = model;
            ItemsSource.Add(viewModel);
        }

        #endregion METHODS
    }
}