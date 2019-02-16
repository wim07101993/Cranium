using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data.Services.Serialization
{
    public class JsonSerializer : ISerializer, IDeserializer
    {
        public string FileExtension { get; } = "json";


        #region deserialization

        public object Deserialize(TextReader reader, Type type, ISerializationOptions options = null)
            => Newtonsoft.Json.JsonSerializer
                .Create(options as JsonSerializationOptions)
                .Deserialize(reader, type);

        public object Deserialize(string serializedValue, Type type, ISerializationOptions options = null)
            => JsonConvert.DeserializeObject(serializedValue, type, options as JsonSerializationOptions);

        public T Deserialize<T>(TextReader reader, ISerializationOptions options = null)
            => Newtonsoft.Json.JsonSerializer
                .Create(options as JsonSerializationOptions)
                .Deserialize<T>(new JsonTextReader(reader));

        public T Deserialize<T>(string serializedValue, ISerializationOptions options = null)
            => JsonConvert.DeserializeObject<T>(serializedValue, options as JsonSerializationOptions);

        public async Task<object> DeserializeAsync(TextReader reader, Type type, ISerializationOptions options = null)
        {
            var result = Deserialize(reader, type, options);
            return await Task.FromResult(result);
        }

        public async Task<object> DeserializeAsync(string serializedValue, Type type, ISerializationOptions options = null)
        {
            var result = Deserialize(serializedValue, type, options);
            return await Task.FromResult(result);
        }

        public async Task<T> DeserializeAsync<T>(TextReader reader, ISerializationOptions options = null)
        {
            var result = Deserialize<T>(reader, options);
            return await Task.FromResult(result);
        }

        public async Task<T> DeserializeAsync<T>(string serializedValue, ISerializationOptions options = null)
        {
            var result = Deserialize<T>(serializedValue, options);
            return await Task.FromResult(result);
        }

        #endregion deserialization


        #region serialization

        public void Serialize(object value, TextWriter writer, ISerializationOptions options = null)
            => Newtonsoft.Json.JsonSerializer
                .Create(options as JsonSerializationOptions)
                .Serialize(writer, value);

        public string Serialize(object value, ISerializationOptions options = null)
            => JsonConvert.SerializeObject(value, options as JsonSerializationOptions);

        public async Task SerializeAsync(object value, TextWriter writer, ISerializationOptions options = null)
        {
            Serialize(value, writer, options);
            await Task.CompletedTask;
        }

        public async Task<string> SerializeAsync(object value, ISerializationOptions options = null)
        {
            var result = Serialize(value, options);
            return await Task.FromResult(result);
        }

        #endregion deserialization
    }
}