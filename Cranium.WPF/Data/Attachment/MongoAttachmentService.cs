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
    public class MongoAttachmentService : AAttachmentService
    {
        private readonly FileAttachmentService _mediaFileService;

        #region FIELDS

        public const int ChunkSize = 1048576;
        private const string BucketName = "Files";

        private readonly GridFSBucket _mediaBucket;

        #endregion FIELDS


        #region CONSTRUCTOR

        public MongoAttachmentService(IMongoDataServiceSettings config, FileAttachmentService mediaFileService)
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