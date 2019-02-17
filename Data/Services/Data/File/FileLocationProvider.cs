using System;
using Data.Services.Serialization;

namespace Data.Services.Data.File
{
    public class FileLocationProvider : IFileLocationProvider
    {
        private readonly ISerializer _serializer;


        public FileLocationProvider(ISerializer serializer)
        {
            _serializer = serializer;
        }


        public string GetLocationOfCollectionFile<T>()
            => $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{typeof(T).Name}.{_serializer.FileExtension}";
    }
}
