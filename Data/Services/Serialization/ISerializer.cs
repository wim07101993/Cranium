using System.IO;
using System.Threading.Tasks;

namespace Data.Services.Serialization
{
    public interface ISerializer
    {
        string FileExtension { get; }

        void Serialize(object value, TextWriter writer, ISerializationOptions options = null);
        string Serialize(object value, ISerializationOptions options = null);

        Task SerializeAsync(object value, TextWriter writer, ISerializationOptions options = null);
        Task<string> SerializeAsync(object value, ISerializationOptions options = null);
    }
}
