using System;
using Prism.Events;

namespace Cranium.WPF.Game
{
    public partial class GameWindow
    {
        public GameWindow(GameWindowViewModel viewModel, IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<CloseAllWindowsEvent>().Subscribe(TryToClose);

            InitializeComponent();
            DataContext = viewModel;
        }

        private void TryToClose()
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                // IGNORED
            }
        }
    }
}
