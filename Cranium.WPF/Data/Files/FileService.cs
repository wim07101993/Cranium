using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cranium.WPF.Helpers.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Cranium.WPF.Data.Files
{
    public class FileService : IFileService
    {
        #region FIELDS

        public const int ChunkSize = 1048576;
        private const string BucketName = "Files";

        private readonly GridFSBucket _mediaBucket;

        #endregion FIELDS


        #region CONSTRUCTOR

        public FileService(IMongoDataServiceSettings config)
        {
            var database = new MongoClient(config.ConnectionString)
                .GetDatabase(config.DatabaseName);

            _mediaBucket = new GridFSBucket(
                database, new GridFSBucketOptions
                {
                    BucketName = BucketName,
                    ChunkSizeBytes = ChunkSize,
                });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public IReadOnlyList<string> ImageExtensions => new[] { ".bmp", ".jpg", ".gif", ".png" };
        public IReadOnlyList<string> MusicExtensions => new[] { ".mp3", ".m4a", ".wma" };
        public IReadOnlyList<string> VideoExtensions => new[] { ".mp4", ".wmv", ".webm" };

        #endregion PROPERTIES


        #region METHDOS

        public async Task<ObjectId> CreateAsync(Stream fileToAdd, string title)
            => await _mediaBucket.UploadFromStreamAsync(title, fileToAdd);

        public async Task GetOneAsync(ObjectId id, Stream outStream) 
            => await _mediaBucket.DownloadToStreamAsync(id, outStream);

        public async Task<byte[]> GetOneAsync(ObjectId id)
            => await _mediaBucket.DownloadAsBytesAsync(id);

        public async Task RemoveAsync(ObjectId objectId)
            => await _mediaBucket.DeleteAsync(objectId);


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