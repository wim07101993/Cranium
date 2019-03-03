using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;

namespace Cranium.WPF.HamburgerMenu
{
    public class HamburgerMenuViewModel : AViewModelBase
    {
        private bool _isOpen;
        private int _selectedView;


        public HamburgerMenuViewModel(IStringsProvider stringsProvider)
            : base(stringsProvider)
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
            set
            {
                if (!SetProperty(ref _selectedView, value))
                    return;
                IsOpen = false;
            }
        }
    }
}