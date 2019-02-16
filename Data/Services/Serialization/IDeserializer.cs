using System;
using System.IO;
using System.Threading.Tasks;

namespace Data.Services.Serialization
{
    public interface IDeserializer
    {
        string FileExtension { get; }

        object Deserialize(TextReader reader, Type type, ISerializationOptions options = null);
        object Deserialize(string serializedValue, Type type, ISerializationOptions options = null);

        T Deserialize<T>(TextReader reader, ISerializationOptions options = null);
        T Deserialize<T>(string serializedValue, ISerializationOptions options = null);

        Task<object> DeserializeAsync(TextReader reader, Type type, ISerializationOptions options = null);
        Task<object> DeserializeAsync(string serializedValue, Type type, ISerializationOptions options = null);

        Task<T> DeserializeAsync<T>(TextReader reader, ISerializationOptions options = null);
        Task<T> DeserializeAsync<T>(string serializedValue, ISerializationOptions options = null);
    }
}
