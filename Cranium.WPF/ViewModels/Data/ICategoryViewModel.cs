using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels
{
    public interface ICategoryViewModel : IModelContainer<Category>
    {
        BitmapImage Image { get; }

        ICommand ChangeImageCommand { get; }

        Task ChangeImageAsync();
    }
}
