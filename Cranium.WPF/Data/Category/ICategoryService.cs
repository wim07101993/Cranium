using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Helpers.Mongo;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Category
{
    public interface ICategoryService : IModelService<Category>
    {
        Task<BitmapImage> GetImageAsync(ObjectId categoryId);
        Task<ObjectId> UpdateImageAsync(ObjectId categoryId, Stream fileStream, string fileName);
    }
}
