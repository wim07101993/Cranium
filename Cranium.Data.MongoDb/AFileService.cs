using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cranium.Data.MongoDb
{
    public abstract class AFileService : AAttachmentFileService
    {
        #region FIELDS

        private readonly GridFSBucket _mediaBucket;

        #endregion FIELDS


        #region CONSTRUCTOR

        protected AFileService(IDataServiceSettings config, string bucketName, int chunkSize = 1048576)
        {
            var database = new MongoClient(config.ConnectionString).GetDatabase(config.DatabaseName);
            
            _mediaBucket = new GridFSBucket(
                database, new GridFSBucketOptions
                {
                    BucketName = bucketName,
                    ChunkSizeBytes = chunkSize,
                });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        #endregion PROPERTIES


        #region METHDOS

        public override async Task<string> CreateAsync(Stream fileToAdd, string title)
        {
            var id = await _mediaBucket.UploadFromStreamAsync(title, fileToAdd);
            return id.ToString();
        }

        public override async Task GetOneAsync(Guid id, Stream outStream)
            => await _mediaBucket.DownloadToStreamAsync(id, outStream);

        public override async Task<byte[]> GetOneAsync(Guid id)
            => await _mediaBucket.DownloadAsBytesAsync(id);

        public override async Task RemoveAsync(Guid objectId)
            => await _mediaBucket.DeleteAsync(objectId);

        #endregion METHODS
    }
}
