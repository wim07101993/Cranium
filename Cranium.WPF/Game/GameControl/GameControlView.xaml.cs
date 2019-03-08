using System;
using System.Linq;
using System.Windows;
using Unity;
using WpfScreenHelper;

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
                _gameWindow = App.UnityContainer.Resolve<GameWindow>();

                var screens = Screen.AllScreens.ToList();
                if (screens.Count > 0)
                {
                    var rect = screens[1].Bounds;
                    _gameWindow.Top = rect.Top;
                    _gameWindow.Left = rect.Left;
                    _gameWindow.Width = rect.Width;
                    _gameWindow.Height = rect.Height;
                }

                _gameWindow.Show();
                
            }
            catch (Exception e)
            {
                // TODO
            }
        }
    }
}