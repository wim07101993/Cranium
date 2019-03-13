using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cranium.WPF.Helpers.Extensions;
using MongoDB.Bson;
using Shared.Extensions;

namespace Cranium.WPF.Helpers.Data.File
{
    public abstract class AFileModelService<T> : IModelService<T>
        where T : IWithId
    {
        private static readonly string DataDir =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{Assembly.GetEntryAssembly().GetName().Name}/{typeof(T).Name}s";

        public async Task<T> CreateAsync(T item)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            item.Id = ObjectId.GenerateNewId();
            await WriteFileAsync(GetPath(item.Id), item);
            return item;
        }

        public async Task<T> GetOneAsync(ObjectId id)
        {
            if (!Directory.Exists(DataDir))
            {
                Directory.CreateDirectory(DataDir);
                return default;
            }

            if (id == default)
                return default;

            var files = Directory.GetFiles(DataDir);
            var filePath = files.First(x => x.Contains(id.ToString()));

            return await ReadFileAsync(filePath);
        }

        public async Task<IList<T>> GetAsync()
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            var files = Directory.GetFiles(DataDir);
            var ret = new List<T>();

            foreach (var filePath in files)
                ret.Add(await ReadFileAsync(filePath));

            return ret;
        }

        public async Task UpdateAsync(T item) 
            => await WriteFileAsync(GetPath(item.Id), item);

        public Task RemoveAsync(ObjectId id)
        {
            if (!Directory.Exists(DataDir))
                Directory.CreateDirectory(DataDir);

            System.IO.File.Delete(GetPath(id));
            return Task.CompletedTask;
        }

        private static async Task<T> ReadFileAsync(string filePath)
        {
            T ret;

            using (var file = System.IO.File.OpenText(filePath))
            {
                var json = await file.ReadToEndAsync();
                ret = json.DeserializeJson<T>();
            }

            return ret;
        }

        private static async Task WriteFileAsync(string filePath, object value)
        {
            using (var file = System.IO.File.Create(filePath))
            {
                var json = value.SerializeJson().ToUtf8();

                await file.WriteAsync(json, 0, json.Length);
                await file.FlushAsync();
            }
        }

        public static string GetPath(ObjectId id) => $"{DataDir}/{id}.json";
    }
}