using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Helpers.Data;
using Cranium.WPF.Helpers.Data.File;
using Cranium.WPF.Strings;
using Prism.Commands;
using Unity;

namespace Cranium.WPF.Helpers.ViewModels
{
    public abstract class ACollectionViewModel<TModel, TViewModel> : AViewModelBase
        where TModel : AWithId
        where TViewModel : AModelContainerViewModel<TModel>
    {
        #region FIELDS

        private readonly IUnityContainer _unityContainer;
        private AFileModelService<TModel> _fileModelService;

        #endregion FIELDS


        #region CONSTRUCTOR

        protected ACollectionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            ModelService = unityContainer.Resolve<IModelService<TModel>>();
            _fileModelService = unityContainer.Resolve<AFileModelService<TModel>>();

            ItemsSource = new ObservableCollection<TViewModel>();

            CreateCommand = new DelegateCommand(async () => await CreateAsync());
            DeleteCommand = new DelegateCommand<TViewModel>(async x => await DeleteAsync(x));
            UpdateCollectionCommand = new DelegateCommand(async () => await UpdateCollectionAsync());
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        protected IModelService<TModel> ModelService { get; }

        public ObservableCollection<TViewModel> ItemsSource { get; }

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCollectionCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public virtual async Task UpdateCollectionAsync()
        {
//            try
//            {
//                var newItems = await ModelService.GetAsync();
//                
//                ItemsSource.Clear();
//                foreach (var model in newItems)
//                    AddModelToCollection(model);
//            }
//            catch (Exception e)
//            {
//                // todo
//                throw e;
//            }
        }

        public virtual async Task CreateAsync()
        {
            var model = await ModelService.CreateAsync(ConstructElement());
            AddModelToCollection(model);
        }

        protected virtual TModel ConstructElement() => Activator.CreateInstance<TModel>();

        public async Task DeleteAsync(TViewModel viewModel)
        {
            try
            {
                await ModelService.RemoveAsync(viewModel.Model.Id);
                ItemsSource.Remove(viewModel);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        protected virtual void AddModelToCollection(TModel model)
        {
            try
            {
                var viewModel = _unityContainer.Resolve<TViewModel>();
                viewModel.Model = model;
                ItemsSource.Add(viewModel);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        #endregion METHODS
    }
}