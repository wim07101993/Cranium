using Cranium.WPF.ViewModels;

namespace Cranium.WPF.Views
{
    public partial class MainWindow
    {
        public MainWindow(IMainWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
