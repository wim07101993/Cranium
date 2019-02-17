using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class GameViewModel : AViewModelBase, IGameViewModel
    {
        public GameViewModel(IStringsProvider stringsProvider)
            : base(stringsProvider)
        {
        }
    }
}
