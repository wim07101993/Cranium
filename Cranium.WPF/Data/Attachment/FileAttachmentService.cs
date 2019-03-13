using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Files
{
    public class FileAttachmentService : AAttachmentService
    {
        private static readonly string DataDir =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{Assembly.GetEntryAssembly().GetName().Name}/Attachments";

        public override async Task<ObjectId> CreateAsync(Stream fileToAdd, string title)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var id = ObjectId.GenerateNewId();

            using (var file = File.Create($"{DataDir}/{id.ToString()}-{title}"))
            using (fileToAdd)
            {
                fileToAdd.Seek(0, SeekOrigin.Begin);
                await fileToAdd.CopyToAsync(file);
            }

            return id;
        }

        public async Task CreateAsync(ObjectId id, Stream fileToAdd, string title)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            using (var file = File.Create($"{DataDir}/{id.ToString()}-{title}"))
            using (fileToAdd)
            {
                fileToAdd.Seek(0, SeekOrigin.Begin);
                await fileToAdd.CopyToAsync(file);
            }

        }

        public override async Task GetOneAsync(ObjectId id, Stream outStream)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var files = Directory.GetFiles(DataDir);
            var filePath = files.First(x => x.Contains(id.ToString()));

            using (var file = File.OpenRead(filePath))
            using (outStream)
            {
                file.Seek(0, SeekOrigin.Begin);
                await file.CopyToAsync(outStream);
            }
        }

        public override async Task<byte[]> GetOneAsync(ObjectId id)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                await GetOneAsync(id, stream);
                bytes = stream.ToArray();
            }

            return bytes;
        }

        public override Task RemoveAsync(ObjectId id)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var files = Directory.GetFiles(DataDir);
            var filePath = files.First(x => x.Contains(id.ToString()));
            File.Delete(filePath);
            return Task.CompletedTask;
        }
    }
}