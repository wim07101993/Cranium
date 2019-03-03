using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Files
{
    public interface IFileService
    {
        IReadOnlyList<string> ImageExtensions { get; }
        IReadOnlyList<string> MusicExtensions { get; }
        IReadOnlyList<string> VideoExtensions { get; }


        Task<ObjectId> CreateAsync(Stream fileToAdd, string title);

        Task GetOneAsync(ObjectId id, Stream outStream);
        Task<byte[]> GetOneAsync(ObjectId id);

        Task RemoveAsync(ObjectId fileId);

        string GenerateImageFilter();
        string GenerateMusicFilter();
        string GenerateVideoFilter();
    }
}
