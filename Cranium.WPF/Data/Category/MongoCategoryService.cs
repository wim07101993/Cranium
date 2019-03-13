using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Data.Attachment;
using Cranium.WPF.Helpers.Data.Mongo;
using Cranium.WPF.Helpers.Extensions;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Category
{
    public class MongoCategoryService : AMongoModelService<Category>, ICategoryService
    {
        private const string CollectionName = "categories";

        private readonly IAttachmentService _fileService;


        public MongoCategoryService(IMongoDataServiceSettings settings, IAttachmentService fileService)
            : base(settings, CollectionName)
        {
            _fileService = fileService;
        }


        public async Task<BitmapImage> GetImageAsync(Category category)
        {
            if (category.Image == default)
                return null;

            var bytes = await _fileService.GetOneAsync(category.Image);
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