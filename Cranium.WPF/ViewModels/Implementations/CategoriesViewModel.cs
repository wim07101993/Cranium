using System.IO;
using System.Windows.Input;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Services.Strings;
using Microsoft.Win32;
using Prism.Commands;

namespace Cranium.WPF.ViewModels.Implementations
{
    public class CategoriesViewModel : DataGridViewModel<Category>, ICategoriesViewModel
    {
        public CategoriesViewModel(IStringsProvider stringsProvider, IClient dataService)
            : base(stringsProvider, dataService)
        {
            EditPictureCommand = new DelegateCommand<Category>(EditPicture);
        }


        public ICommand EditPictureCommand { get; }


        public void EditPicture(Category category)
        {
            var dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            category.Image = File.ReadAllBytes(dialog.FileName);
        }
    }
}