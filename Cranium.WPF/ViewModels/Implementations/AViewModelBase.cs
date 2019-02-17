using Cranium.WPF.Services.Strings;
using Prism.Mvvm;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class AViewModelBase : BindableBase, IViewModelBase
    {
        public AViewModelBase(IStringsProvider stringsProvider)
        {
            Strings = stringsProvider.Strings;
        }

        public Strings Strings { get; }
    }
}
