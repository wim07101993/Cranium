using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace Cranium.WPF.Data.Category
{
    public class CategoryViewModel : AModelContainerViewModel<Category>
    {
        #region FIELDS

        private readonly ICategoryService _categoryService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAttachmentService _attachmentService;

        private BitmapImage _image;
        private string _imagePath;

        #endregion FIELDS


        #region CONSTRUCTOR

        public CategoryViewModel(
            IStringsProvider stringsProvider, ICategoryService categoryService, IEventAggregator eventAggregator,
            IAttachmentService attachmentService)
            : base(stringsProvider)
        {
            _categoryService = categoryService;
            _eventAggregator = eventAggregator;
            _attachmentService = attachmentService;

            ChangeImageCommand = new DelegateCommand(() =>
            {
                var _ = ChangeImageAsync();
            });

            var __ = GetImageAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public BitmapImage Image
        {
            get => _image;
            private set => SetProperty(ref _image, value);
        }

        public ICommand ChangeImageCommand { get; }

        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        #endregion PROPERTIES


        #region METHODS

        public async Task GetImageAsync()
        {
            if (Model == null || Model.Image == default)
            {
                Image = null;
                return;
            }

            try
            {
                Image = await _categoryService.GetImageAsync(Model);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async Task ChangeImageAsync()
        {
            await Task.CompletedTask;

            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile,
                Filter = $"{_attachmentService.GenerateImageFilter()}|" +
                         $"{_attachmentService.GenerateMusicFilter()}|" +
                         $"{_attachmentService.GenerateVideoFilter()}"
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            ImagePath = dialog.FileName;

            using (var file = File.Open(ImagePath, FileMode.Open, FileAccess.Read))
                Image = file.ToImage();
        }

        protected override async Task OnModelChangedAsync()
        {
            await GetImageAsync();
        }
        
        private void UpdateColor(Category category)
        {
            if (category.Color.A == 0 ||
                category.Color.A == 255 && category.Color.R == 255 &&
                category.Color.G == 255 && category.Color.B == 255 ||
                category.Color.A == 255 && category.Color.R == 0 &&
                category.Color.G == 0 && category.Color.B == 0 ||
                category.Color == default)
                category.Color = new Color {BaseColor = Image?.GetAverageColorAsync().ToMediaColor() ?? default};
        }

        #endregion METHODS
    }
}