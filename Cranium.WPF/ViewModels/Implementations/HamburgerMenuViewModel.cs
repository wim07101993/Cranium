using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class HamburgerMenuViewModel : AViewModelBase, IHamburgerMenuViewModel
    {
        private bool _isOpen;
        private int _selectedView;


        public HamburgerMenuViewModel(IStringsProvider stringsProvider) : base(stringsProvider)
        {
        }


        public bool IsOpen
        {
            get => _isOpen;
            set => SetProperty(ref _isOpen, value);
        }

        public int SelectedView
        {
            get => _selectedView;
            set => SetProperty(ref _selectedView, value);
        }
    }
}