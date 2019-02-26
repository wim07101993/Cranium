using Cranium.WPF.Models;
using Cranium.WPF.ViewModels.Implementations;
using Unity;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public sealed class CategoriesViewModel : ACollectionViewModel<Category, ICategoryViewModel>, ICategoriesViewModel
    {
        public CategoriesViewModel(IUnityContainer unityContainer) : base(unityContainer)
        {
            var _ = UpdateCollectionAsync();
        }
    }
}