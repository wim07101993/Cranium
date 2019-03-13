using Cranium.WPF.Helpers.ViewModels;
using Prism.Events;
using System.Threading.Tasks;
using Unity;

namespace Cranium.WPF.Data.Category
{
    public sealed class CategoriesViewModel : ACollectionViewModel<Category, CategoryViewModel>
    {
        #region FIELDS

        private readonly IEventAggregator _eventAggregator;

        #endregion FIELDS


        #region CONSTRUCTOR

        public CategoriesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            var _ = UpdateCollectionAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        #endregion PROPERTIES


        #region METHODS

        public override async Task SaveAsync(CategoryViewModel viewModel)
        {
            await base.SaveAsync(viewModel);
            _eventAggregator.GetEvent<CategoryChangedEvent>().Publish(viewModel.Model);
        }

        #endregion METHODS
    }
}