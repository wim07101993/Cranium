using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cranium.WPF.Helpers.Data.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Unity;

namespace Cranium.WPF.Data.Files
{
    public class MongoFileService : AFileService
    {
        private readonly MediaFileService _mediaFileService;

        #region FIELDS

        public const int ChunkSize = 1048576;
        private const string BucketName = "Files";

        private readonly GridFSBucket _mediaBucket;

        #endregion FIELDS


        #region CONSTRUCTOR

        public MongoFileService(IMongoDataServiceSettings config, MediaFileService mediaFileService)
        {
            _mediaFileService = mediaFileService;
            var database = new MongoClient(config.ConnectionString)
                .GetDatabase(config.DatabaseName);

            _mediaBucket = new GridFSBucket(
                database, new GridFSBucketOptions
                {
                    BucketName = BucketName,
                    ChunkSizeBytes = ChunkSize,
                });

            var _ = CopyAsync();
        }

        private async Task CopyAsync()
        {
            var files = await _mediaBucket.Find(new ExpressionFilterDefinition<GridFSFileInfo>(x => true))
                .ToListAsync();

            foreach (var fileInfo in files)
            {
                var existingPaths = Directory.GetFiles(@"C:\Users\wimva\AppData\Roaming\Cranium\Attachments");
                if (existingPaths.Any(x => x.Contains(fileInfo.Filename)))
                    continue;
                using (var stream = new MemoryStream())
                {
                    await GetOneAsync(fileInfo.Id, stream);
                    await _mediaFileService.CreateAsync(fileInfo.Id, stream, fileInfo.Filename);
                }
            }
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        #endregion PROPERTIES


        #region METHDOS

        public override async Task<ObjectId> CreateAsync(Stream fileToAdd, string title)
            => await _mediaBucket.UploadFromStreamAsync(title, fileToAdd);

        public override async Task GetOneAsync(ObjectId id, Stream outStream)
            => await _mediaBucket.DownloadToStreamAsync(id, outStream);

        public override async Task<byte[]> GetOneAsync(ObjectId id)
            => await _mediaBucket.DownloadAsBytesAsync(id);

        public override async Task RemoveAsync(ObjectId objectId)
            => await _mediaBucket.DeleteAsync(objectId);

        #endregion METHODS
    }
}