using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Cranium.WPF.Services.Mongo
{
    public interface IFileService
    {
        Task<ObjectId> CreateAsync(Stream fileToAdd, string title);

        Task GetOneAsync(ObjectId id, Stream outStream);

        Task RemoveAsync(ObjectId fileId);
    }
}
