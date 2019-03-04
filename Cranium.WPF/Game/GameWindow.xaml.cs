namespace Cranium.WPF.Game
{
    public partial class GameWindow
    {
        public GameWindow(GameWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
