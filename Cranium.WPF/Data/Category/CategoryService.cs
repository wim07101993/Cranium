using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Extensions;
using Cranium.WPF.Helpers.Mongo;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Category
{
    public class CategoryService : AMongoModelService<Category>, ICategoryService
    {
        private const string CollectionName = "categories";

        private readonly IFileService _fileService;


        public CategoryService(IMongoDataServiceSettings settings, IFileService fileService)
            : base(settings, CollectionName)
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