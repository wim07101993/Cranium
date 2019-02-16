using System.IO;
using System.Threading.Tasks;
using Data.Services.Serialization;

namespace Data.Services.Files
{
    public interface IFileService
    {
        Task<string> ReadTextAsync(string path);
        Task<T> ReadAsync<T>(string path, IDeserializer deserializer = null);
        Task<byte[]> ReadBytesAsync(string path, int skip = 0, int take = -1);
        Task<byte[][]> ReadBigBytesAsync(string path, int skip = 0, long take = -1);
        TextReader Read(string path);

        Task WriteLinesAsync(string path, params string[] text);
        Task WriteAsync(string path, object item, ISerializer serializer = null);
        Task WriteBytesAsync(string path, byte[] bytes);
        TextWriter Write(string path);
    }
}
