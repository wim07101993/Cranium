using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Data
{
    public interface ICategoryViewModel : IModelContainer<Category>
    {
        BitmapImage Image { get; }

        ICommand ChangeImageCommand { get; }

        Task ChangeImageAsync();
    }
}
