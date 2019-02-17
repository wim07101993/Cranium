using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Common;
using Data.Exceptions;
using Data.Models.Bases;
using Data.Services.Files;
using Shared.Extensions;

namespace Data.Services.Data.File
{
    public abstract class ADataService<T> : IDataService<T>
        where T : IWithId
    {
        private readonly IFileService _fileService;
        private readonly IFileLocationProvider _fileLocationProvider;


        protected ADataService(IFileService fileService, IFileLocationProvider fileLocationProvider)
        {
            _fileService = fileService;
            _fileLocationProvider = fileLocationProvider;
        }


        public async Task CreateAsync(T item)
        {
            item.Id = await GenerateIdAsync();

            var items = await GetAsync();
            items.Add(item);

            await _fileService.WriteAsync(_fileLocationProvider.GetLocationOfCollectionFile<T>(), item);
        }

        public async Task<T> GetAsync(long id)
        {
            try
            {
                var elements = await GetAsync(dataRequest: new DataRequest<T> {Where = x => x.Id == id});
                return elements.Single();
            }
            catch (InvalidOperationException)
            {
                throw new IdNotFoundException(id, typeof(T));
            }
        }

        public async Task<IList<T>> GetAsync(int skip = 0, int take = -1, DataRequest<T> dataRequest = null)
        {
            IEnumerable<T> collection = await _fileService.ReadAsync<List<T>>(_fileLocationProvider.GetLocationOfCollectionFile<T>());
            collection = collection
                ?.Skip(skip)
                .Take(take);
            
            if (dataRequest?.Where != null)
                collection = collection?.Where(dataRequest.Where.Compile());
            if (dataRequest?.OrderBy != null)
                collection = collection?.OrderBy(dataRequest.OrderBy.Compile());
            if (dataRequest?.OrderByDesc != null)
                collection = collection?.OrderByDescending(dataRequest.OrderByDesc.Compile());

            return collection?.ToList();
        }

        public async Task<IList<TOut>> SelectAsync<TOut>(
            Func<T, TOut> selector, int skip = 0, int take = -1, DataRequest<T> dataRequest = null)
        {
            var collection = await GetAsync(skip, take, dataRequest);
            return collection?.Select(selector).ToList();
        }

        public async Task<int> GetCountAsync(DataRequest<T> dataRequest)
        {
            var collection = await GetAsync(dataRequest: dataRequest);
            return collection.Count;
        }

        public async Task UpdateAsync(T item)
        {
            var collection = await GetAsync();
            var index = collection.IndexOfFirst(x => x.Id == item.Id);
            if (index == -1)
                throw new IdNotFoundException(item.Id, typeof(T));

            collection[index] = item;
            await _fileService.WriteAsync(_fileLocationProvider.GetLocationOfCollectionFile<T>(), collection);
        }

        public async Task DeleteAsync(params T[] items)
        {
            var collection = await GetAsync();
            if (!collection.RemoveWhere(x => items.Any(y => x.Id == y.Id)))
                throw new IdNotFoundException("There where no items found to delete");
            
            await _fileService.WriteAsync(_fileLocationProvider.GetLocationOfCollectionFile<T>(), collection);
        }

        protected virtual async Task<bool> ExistsAsync(long id)
        {
            try
            {
                await GetAsync(id);
            }
            catch (IdNotFoundException)
            {
                return false;
            }

            return true;
        }

        protected virtual async Task<long> GenerateIdAsync()
        {
            var id = UidGenerator.Next();
            while (await ExistsAsync(id))
                id = UidGenerator.Next();
            return id;
        }


        public virtual void Dispose()
        {
        }
    }
}