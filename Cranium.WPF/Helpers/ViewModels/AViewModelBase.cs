using Cranium.WPF.Strings;
using Prism.Mvvm;

namespace Cranium.WPF.Helpers.ViewModels
{
    public class AViewModelBase : BindableBase
    {
        public AViewModelBase(IStringsProvider stringsProvider)
        {
            Strings = stringsProvider.Strings;
        }

        public Strings.Strings Strings { get; }
    }
}
