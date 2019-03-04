using System;
using System.ComponentModel;
using System.Windows;
using Cranium.WPF.HamburgerMenu;
using Prism.Events;

namespace Cranium.WPF
{
    public partial class MainWindow
    {
        #region FIELDS

        private readonly IEventAggregator _eventAggregator;

        #endregion FIELDS


        #region CONSTRUCTOR

        public MainWindow(MainWindowViewModel viewModel, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;

            DataContext = viewModel;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;

        #endregion PROPERTIES


        #region METHODS

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            try
            {
                _eventAggregator.GetEvent<CloseAllWindowsEvent>().Publish();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion METHODS
    }
}