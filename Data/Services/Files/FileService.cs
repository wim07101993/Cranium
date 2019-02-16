using System;
using System.IO;
using System.Threading.Tasks;
using Data.Services.Serialization;
using Newtonsoft.Json;

namespace Data.Services.Files
{
    public class FileService : IFileService
    {
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;


        public FileService(ISerializer serializer, IDeserializer deserializer)
        {
            _serializer = serializer;
            _deserializer = deserializer;
        }


        #region reading

        public async Task<string> ReadTextAsync(string path)
        {
            using (var reader = Read(path))
                return await reader.ReadToEndAsync();
        }

        public async Task<T> ReadAsync<T>(string path, IDeserializer deserializer = null)
        {
            var d = deserializer ?? _deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            using (var reader = Read(path))
                return await d.DeserializeAsync<T>(reader);
        }

        public async Task<byte[]> ReadBytesAsync(string path, int skip = 0, int take = -1)
        {
            using (var reader = (StreamReader) Read(path))
            {
                var lLength = take == -1
                    ? reader.BaseStream.Length
                    : take;

                if (lLength > int.MaxValue)
                    throw new OverflowException(
                        $"The size of the file is larger than the maximum size of an array. Use {nameof(ReadBigBytesAsync)} instead.");

                var iLength = (int) lLength;
                var buff = new byte[lLength];
                await reader.BaseStream.ReadAsync(buff, 0, iLength);
                return buff;
            }
        }

        public Task<byte[][]> ReadBigBytesAsync(string path, int skip = 0, long take = -1)
            => throw new NotImplementedException();

        public TextReader Read(string path)
            => new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096,
                FileOptions.Asynchronous));

        #endregion reading

        #region writing

        public async Task WriteLinesAsync(string path, params string[] text)
        {
            using (var writer = Write(path))
            {
                foreach (var s in text)
                    await writer.WriteLineAsync(s);
                await writer.FlushAsync();
            }
        }

        public async Task WriteAsync(string path, object item, ISerializer serializer = null)
        {
            var s = serializer ?? _serializer ?? throw new ArgumentNullException(nameof(serializer));
            using (var writer = Write(path))
                await s.SerializeAsync(item, writer,
                    new JsonSerializationOptions {Formatting = Formatting.Indented});
        }

        public async Task WriteBytesAsync(string path, byte[] bytes)
        {
            using (var writer = (StreamWriter) Write(path))
            {
                await writer.BaseStream.WriteAsync(bytes, 0, bytes.Length);
                await writer.FlushAsync();
            }
        }

        public TextWriter Write(string path)
            => new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096,
                FileOptions.Asynchronous));

        #endregion writing
    }
}