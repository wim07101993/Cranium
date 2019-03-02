using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class FileService : IFileService
    {
        private readonly GridFSBucket _mediaBucket;

        public const int ChunkSize = 1048576;


        public FileService(IMongoDataServiceSettings config)
        {
            var database = new MongoClient(config.ConnectionString)
                .GetDatabase(config.DatabaseName);

            _mediaBucket = new GridFSBucket(
                database, new GridFSBucketOptions
                {
                    BucketName = "Files",
                    ChunkSizeBytes = ChunkSize,
                });
        }


        public async Task<ObjectId> CreateAsync(Stream fileToAdd, string title)
            => await _mediaBucket.UploadFromStreamAsync(title, fileToAdd);

        public async Task GetOneAsync(ObjectId id, Stream outStream)
            => await _mediaBucket.DownloadToStreamAsync(id, outStream);

        public async Task<byte[]> GetOneAsync(ObjectId id) 
            => await _mediaBucket.DownloadAsBytesAsync(id);

        public async Task RemoveAsync(ObjectId objectId)
            => await _mediaBucket.DeleteAsync(objectId);
    }
}
