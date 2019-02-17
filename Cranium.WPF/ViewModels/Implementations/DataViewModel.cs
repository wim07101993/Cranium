using System.ComponentModel;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class DataViewModel : AViewModelBase, IDataViewModel
    {
        public DataViewModel(IStringsProvider stringsProvider) 
            : base(stringsProvider)
        {
        }
    }
}
