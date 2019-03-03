namespace Cranium.WPF.Game.Control
{
    public partial class ControlWindow
    {
        public ControlWindow(ControlWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
