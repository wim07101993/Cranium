using System.Windows.Input;
using Cranium.Data.RestClient.Models;

namespace Cranium.WPF.ViewModels
{
    public interface ICategoriesViewModel : IDataGridViewModel<Category>
    {
        ICommand EditPictureCommand { get; }

        void EditPicture(Category category);
    }
}