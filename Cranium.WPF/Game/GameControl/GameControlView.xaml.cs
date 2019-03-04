using System;
using System.Windows;
using Unity;

namespace Cranium.WPF.Game.GameControl
{
    public partial class GameControlView
    {
        private GameWindow _gameWindow;

        public GameControlView()
        {
            InitializeComponent();
        }


        private void OnOpenGameWindowClicked(object sender, RoutedEventArgs arg)
        {
            try
            {
                if (_gameWindow == null)
                    _gameWindow = App.UnityContainer.Resolve<GameWindow>();
                _gameWindow.Show();
            }
            catch (Exception e)
            {
                // TODO
            }
        }
    }
}