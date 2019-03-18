using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cranium.Data
{
    public abstract class AAttachmentService : IAttachmentService
    {
        #region PROPERTIES

        public IReadOnlyList<string> ImageExtensions => new[] { ".bmp", ".jpg", ".gif", ".png" };
        public IReadOnlyList<string> MusicExtensions => new[] { ".mp3", ".m4a", ".wma" };
        public IReadOnlyList<string> VideoExtensions => new[] { ".mp4", ".wmv", ".webm" };

        #endregion PROPERTIES


        #region METHDOS

        public abstract Task<string> CreateAsync(Stream fileToAdd, string title);

        public virtual async Task<string> CreateAsync(string filePath)
        {
            var stream = File.OpenRead(filePath);
            return await CreateAsync(stream, Path.GetFileName(filePath));
        }

        public abstract Task GetOneAsync(Guid id, Stream outStream);

        public abstract Task<byte[]> GetOneAsync(Guid id);

        public abstract Task RemoveAsync(Guid objectId);


        public string GenerateImageFilter()
        {
            var imageExtensions = new StringBuilder("Image files (");

            foreach (var imageExtension in ImageExtensions)
                imageExtensions.Append($"*{imageExtension};");

            imageExtensions.Append(")|");

            foreach (var imageExtension in ImageExtensions)
                imageExtensions.Append($"*{imageExtension};");

            return imageExtensions.ToString();
        }

        public string GenerateMusicFilter()
        {
            var musicExtensions = new StringBuilder("Music files (");

            foreach (var musicExtension in MusicExtensions)
                musicExtensions.Append($"*{musicExtension};");

            musicExtensions.Append(")|");

            foreach (var musicExtension in MusicExtensions)
                musicExtensions.Append($"*{musicExtension};");

            return musicExtensions.ToString();
        }

        public string GenerateVideoFilter()
        {
            var videoExtensions = new StringBuilder("Video files (");

            foreach (var videoExtension in VideoExtensions)
                videoExtensions.Append($"*{videoExtension};");

            videoExtensions.Append(")|");

            foreach (var videoExtension in VideoExtensions)
                videoExtensions.Append($"*{videoExtension};");

            return videoExtensions.ToString();
        }

        #endregion METHODS
    }
}
