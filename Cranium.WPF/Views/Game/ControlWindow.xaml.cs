using Cranium.WPF.ViewModels.Game;

namespace Cranium.WPF.Views.Game
{
    public partial class ControlWindow
    {
        public ControlWindow(IControlWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
