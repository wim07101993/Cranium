using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using Cranium.WPF.Services.Mongo;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;
using Microsoft.Win32;
using Prism.Commands;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class CategoryViewModel : AViewModelBase, ICategoryViewModel
    {
        #region FIELDS

        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        private Category _model;
        private BitmapImage _image;

        #endregion FIELDS


        #region CONSTRUCTOR

        public CategoryViewModel(
            IStringsProvider stringsProvider, IFileService fileService, ICategoryService categoryService)
            : base(stringsProvider)
        {
            _fileService = fileService;
            _categoryService = categoryService;
            ChangeImageCommand = new DelegateCommand(async () => await ChangeImageAsync());
            GetImageFromDb();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public Category Model
        {
            get => _model;
            set
            {
                if (Equals(_model, value))
                    return;

                if (_model != null)
                    value.PropertyChanged -= OnCategoryPropertyChanged;

                SetProperty(ref _model, value);

                GetImageFromDb();

                if (value != null)
                    value.PropertyChanged += OnCategoryPropertyChanged;
            }
        }

        public BitmapImage Image
        {
            get => _image;
            private set => SetProperty(ref _image, value);
        }

        public ICommand ChangeImageCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetImageFromDb()
        {
            if (Model == null || Model.Image == default)
            {
                Image = null;
                return;
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await _fileService.GetOneAsync(_model.Image, stream);
                    Image = stream.ToImage();
                }
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async Task ChangeImageAsync()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            if (Model.Image != default)
                await _fileService.RemoveAsync(Model.Image);

            var stream = File.OpenRead(dialog.FileName);
            var fileName = Path.GetFileName(dialog.FileName);

            var imgId = await _fileService.CreateAsync(stream, fileName);
            Model.Image = imgId;
        }

        private void OnCategoryPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCategoryPropertyChangedAsync(sender as Category, e.PropertyName);
        }

        private async Task OnCategoryPropertyChangedAsync(Category item, string propertyName)
        {
            if (item == null)
                return;

            switch (propertyName)
            {
                case nameof(Model.Color):
                    await _categoryService.UpdatePropertyAsync(item.Id, x => x.Color, item.Color);
                    break;
                case nameof(Model.Description):
                    await _categoryService.UpdatePropertyAsync(item.Id, x => x.Description, item.Description);
                    break;
                case nameof(Model.Image):
                    await _categoryService.UpdatePropertyAsync(item.Id, x => x.Image, item.Image);
                    UpdateColor(item);
                    break;
                case nameof(Model.Name):
                    await _categoryService.UpdatePropertyAsync(item.Id, x => x.Name, item.Name);
                    break;
            }
        }

        private void UpdateColor(Category category)
        {
            if (category.Color.A == 255 && category.Color.R == 255 && category.Color.G == 255 &&
                category.Color.B == 255 ||
                category.Color.A == 255 && category.Color.R == 0 && category.Color.G == 0 && category.Color.B == 0 ||
                category.Color == default)
                category.Color = new Color {BaseColor = Image?.GetAverageColorAsync().ToMediaColor() ?? default};
        }

        #endregion METHODS
    }
}