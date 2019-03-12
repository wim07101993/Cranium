using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Cranium.WPF.Helpers.Data;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Category
{
    public interface ICategoryService : IModelService<Category>
    {
        Task<BitmapImage> GetImageAsync(Category category);
        Task<ObjectId> UpdateImageAsync(Category category, Stream fileStream, string fileName);
    }
}
