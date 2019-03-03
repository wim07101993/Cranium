using Cranium.WPF.Helpers.ViewModels;
using Unity;

namespace Cranium.WPF.Data.Category
{
    public sealed class CategoriesViewModel : ACollectionViewModel<Category, CategoryViewModel>
    {
        public CategoriesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            var _ = UpdateCollectionAsync();
        }
    }
}