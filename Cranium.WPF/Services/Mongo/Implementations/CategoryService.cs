using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Extensions;
using Cranium.WPF.Models;
using MongoDB.Bson;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class CategoryService : AMongoModelService<Category>, ICategoryService
    {
        private readonly IFileService _fileService;


        public CategoryService(IMongoDataServiceSettings settings, IFileService fileService)
            : base(settings, "categories")
        {
            _fileService = fileService;
        }


        public async Task<BitmapImage> GetImageAsync(ObjectId categoryId)
        {
            var imageId = await GetPropertyAsync(categoryId, x => x.Image);
            if (imageId == default)
                return null;

            var bytes = await _fileService.GetOneAsync(imageId);
            return bytes.ToImage();
        }

        public async Task<ObjectId> UpdateImageAsync(ObjectId categoryId, Stream fileStream, string fileName)
        {
            var oldImageId = await GetPropertyAsync(categoryId, x => x.Image);
            if (oldImageId != default)
                await _fileService.RemoveAsync(oldImageId);

            var newImageId = await _fileService.CreateAsync(fileStream, fileName);

            await UpdatePropertyAsync(categoryId, x => x.Image, newImageId);

            return newImageId;
        }
    }
}