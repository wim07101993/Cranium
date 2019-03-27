using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cranium.Data
{
    public interface IAttachmentFileService
    {
        IReadOnlyList<string> ImageExtensions { get; }
        IReadOnlyList<string> MusicExtensions { get; }
        IReadOnlyList<string> VideoExtensions { get; }


        Task<string> CreateAsync(Stream attachmentToAdd, string title);
        Task<string> CreateAsync(string filePath);

        Task GetOneAsync(Guid id, Stream outStream);
        Task<byte[]> GetOneAsync(Guid id);

        Task RemoveAsync(Guid attachmentId);

        string GenerateImageFilter();
        string GenerateMusicFilter();
        string GenerateVideoFilter();
    }
}
