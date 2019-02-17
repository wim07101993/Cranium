using System.ComponentModel;
using System.Windows;
using Cranium.WPF.ViewModels;

namespace Cranium.WPF.Views
{
    public partial class MainWindow
    {
        public MainWindow(IMainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChagned;

            DataContext = viewModel;
        }


        public IMainWindowViewModel ViewModel => DataContext as IMainWindowViewModel;


        private void OnDataContextChagned(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            if (ViewModel.HamburgerMenuViewModel != null)
                ViewModel.HamburgerMenuViewModel.PropertyChanged += OnHamburgerMenuViewModelProprtyChanged;

            UpdateVisibilities();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IMainWindowViewModel.HamburgerMenuViewModel):
                    ViewModel.HamburgerMenuViewModel.PropertyChanged += OnHamburgerMenuViewModelProprtyChanged;
                    UpdateVisibilities();
                    break;
            }
        }
        
        private void OnHamburgerMenuViewModelProprtyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IHamburgerMenuViewModel.SelectedView):
                    UpdateVisibilities();
                    break;
            }
        }

        private void UpdateVisibilities()
        {
            switch (ViewModel?.HamburgerMenuViewModel?.SelectedView)
            {
                case 0:
                    Game.Visibility = Visibility.Visible;
                    Data.Visibility = Visibility.Collapsed;
                    Settings.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    Game.Visibility = Visibility.Collapsed;
                    Data.Visibility = Visibility.Visible;
                    Settings.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Game.Visibility = Visibility.Collapsed;
                    Data.Visibility = Visibility.Collapsed;
                    Settings.Visibility = Visibility.Visible;
                    break;
                case null:
                    break;
            }
        }
    }
}
