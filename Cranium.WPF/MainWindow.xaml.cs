using System;
using System.ComponentModel;
using System.Windows;
using Cranium.WPF.Game.Control;
using Cranium.WPF.HamburgerMenu;

namespace Cranium.WPF
{
    public partial class MainWindow
    {
        private readonly ControlWindow _controlWindow;


        public MainWindow(MainWindowViewModel viewModel, ControlWindow controlWindow)
        {
            _controlWindow = controlWindow;
            InitializeComponent();

            DataContextChanged += OnDataContextChagned;

            DataContext = viewModel;

            _controlWindow.Show();
            _controlWindow.Closing += OnControlWindowClosing;
        }

        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;


        private void OnDataContextChagned(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel == null)
                return;

            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            if (ViewModel.HamburgerMenuViewModel != null)
                ViewModel.HamburgerMenuViewModel.PropertyChanged += OnHamburgerMenuViewModelPropertyChanged;

            UpdateVisibilities();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MainWindowViewModel.HamburgerMenuViewModel):
                    ViewModel.HamburgerMenuViewModel.PropertyChanged += OnHamburgerMenuViewModelPropertyChanged;
                    UpdateVisibilities();
                    break;
            }
        }
        
        private void OnHamburgerMenuViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(HamburgerMenuViewModel.SelectedView):
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


        private void OnControlWindowClosing(object sender, CancelEventArgs e) => Close();

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                _controlWindow.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
