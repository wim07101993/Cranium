using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Models;
using MongoDB.Bson;

namespace Cranium.WPF.Services.Mongo
{
    public interface ICategoryService : IModelService<Category>
    {
        Task<BitmapImage> GetImageAsync(ObjectId categoryId);
        Task<ObjectId> UpdateImageAsync(ObjectId categoryId, Stream fileStream, string fileName);
    }
}
