using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Helpers.Data;
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
        
        #endregion FIELDS


        #region CONSTRUCTOR

        protected ACollectionViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            _unityContainer = unityContainer;
            ModelService = unityContainer.Resolve<IModelService<TModel>>();
           
            CreateCommand = new DelegateCommand(() => 
            {
                var _ = CreateAsync();
            });
            DeleteCommand = new DelegateCommand<TViewModel>(x =>
            {
                var _ = DeleteAsync(x);
            });
            UpdateCollectionCommand = new DelegateCommand(() =>
            {
                var _ = UpdateCollectionAsync();
            });
            SaveCommand = new DelegateCommand<TViewModel>(x =>
            {
                var _ = SaveAsync(x);
            });
            SaveAllCommand = new DelegateCommand(() =>
            {
                var _ = SaveAllAsync();
            });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        protected IModelService<TModel> ModelService { get; }

        public ObservableCollection<TViewModel> ItemsSource { get; } = new ObservableCollection<TViewModel>();

        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCollectionCommand { get; }

        public ICommand SaveCommand { get; }
        public ICommand SaveAllCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public virtual async Task UpdateCollectionAsync()
        {
            try
            {
                var newItems = await ModelService.GetAsync();

                ItemsSource.Clear();
                foreach (var model in newItems)
                {
                    model.ChangedProperties.Clear();
                    AddModelToCollection(model);
                }
            }
            catch (Exception e)
            {
                // todo
            }
        }

        public virtual async Task CreateAsync()
        {
            try
            {
                var model = await ModelService.CreateAsync(ConstructElement());
                model.ChangedProperties.Clear();
                AddModelToCollection(model);
            }
            catch (Exception e)
            {
                // TODO
            }
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

        public virtual async Task SaveAsync(TViewModel viewModel)
        {
            try
            {
                await ModelService.UpdateAsync(viewModel.Model);
            }
            catch (Exception e)
            {
                // TODO 
            }
        }

        public virtual async Task SaveAllAsync()
        {
            try
            {
                foreach (var item in ItemsSource)
                    await SaveAsync(item);
            }
            catch (Exception e)
            {
                // TODO 
            }
        }

        #endregion METHODS
    }
}