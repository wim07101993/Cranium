using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Cranium.Data.RestClient.Models;
using Cranium.Data.RestClient.Services;
using Cranium.WPF.Extensions;
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

        protected override void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var category = (Category) sender;
            switch (e.PropertyName)
            {
                case nameof(Category.Image):
                    UpdateColor(category);
                    break;
            }

            base.OnItemPropertyChanged(sender, e);
        }

        protected override void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsCollectionChanged(sender, e);

            foreach (var category in e.NewItems.Cast<Category>())
                UpdateColor(category);
        }


        private static void UpdateColor(Category category)
        {
            if (category.Color.A == 255 && category.Color.R == 255 && category.Color.G == 255 && category.Color.B == 255 ||
                category.Color.A == 255 && category.Color.R == 0 && category.Color.G == 0 && category.Color.B == 0 ||
                category.Color == Color.Empty)
                category.Color = category.Image?.ToImage().GetAverageColorAsync() ?? Color.Empty;
        }
    }
}