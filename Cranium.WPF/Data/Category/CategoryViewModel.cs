using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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

        private BitmapImage _image;

        #endregion FIELDS


        #region CONSTRUCTOR

        public CategoryViewModel(
            IStringsProvider stringsProvider, ICategoryService categoryService, IEventAggregator eventAggregator)
            : base(stringsProvider)
        {
            _categoryService = categoryService;
            _eventAggregator = eventAggregator;

            ChangeImageCommand = new DelegateCommand(async () => await ChangeImageAsync());

            var _ = GetImageFromDbAsync();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public BitmapImage Image
        {
            get => _image;
            private set => SetProperty(ref _image, value);
        }

        public ICommand ChangeImageCommand { get; }

        #endregion PROPERTIES


        #region METHODS

        protected override async Task OnModelChangedAsync()
        {
            await GetImageFromDbAsync();
        }

        protected override async Task OnModelPropertyChangedAsync(Category category, string propertyName)
        {
            if (category == null)
                return;

            switch (propertyName)
            {
                case nameof(Model.Color):
                    await _categoryService.UpdateAsync(category);
                    break;
                case nameof(Model.Description):
                    await _categoryService.UpdateAsync(category);
                    break;
                case nameof(Model.Image):
                    await GetImageFromDbAsync();
                    UpdateColor(category);
                    break;
                case nameof(Model.Name):
                    await _categoryService.UpdateAsync(category);
                    break;
            }

            _eventAggregator.GetEvent<CategoryChangedEvent>().Publish(category);
        }

        public async Task GetImageFromDbAsync()
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
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Title = Strings.SelectAnImageFile
            };
            dialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(dialog.FileName))
                return;

            var stream = File.OpenRead(dialog.FileName);
            var fileName = Path.GetFileName(dialog.FileName);

            try
            {
                var imgId = await _categoryService.UpdateImageAsync(Model, stream, fileName);
                Model.Image = imgId;
            }
            catch (Exception e)
            {
                // TODO
            }
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