using System.Windows;
using Cranium.WPF.Views;

namespace Cranium.WPF
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
