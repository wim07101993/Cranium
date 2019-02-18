using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class CategoriesViewModel : DataGridViewModel<Category>, ICategoriesViewModel
    {
        public CategoriesViewModel(IStringsProvider stringsProvider, IClient dataService)
            : base(stringsProvider, dataService)
        {
        }
    }
}