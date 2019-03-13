using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Attachment;
using Cranium.WPF.Helpers.Data.File;
using Cranium.WPF.Helpers.Extensions;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Category
{
    public class FileCategoryService : AFileModelService<Category>, ICategoryService
    {
        private readonly FileAttachmentService _fileService;

        public FileCategoryService(FileAttachmentService fileService)
        {
            _fileService = fileService;
        }


        public async Task<BitmapImage> GetImageAsync(Category category)
        {
            var bytes =  await _fileService.GetOneAsync(category.Image);
            return bytes.ToImage();
        }

        public async Task<ObjectId> UpdateImageAsync(Category category, Stream fileStream, string fileName)
        {
            var oldCategory = await GetOneAsync(category.Id);
            if (oldCategory.Image != default)
                await _fileService.RemoveAsync(oldCategory.Image);

            var newImageId = await _fileService.CreateAsync(fileStream, fileName);
            category.Image = newImageId;
            await UpdateAsync(category);

            return newImageId;
        }
    }
}
