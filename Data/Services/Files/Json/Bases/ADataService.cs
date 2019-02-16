using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Data.Models.Bases;

namespace Data.Services.Files.Json.Bases
{
    public abstract class ADataService<T> : IDataService<T>
        where T : IWithId
    {
        public Task<bool> CreateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetAsync(int skip, int take, DataRequest<T> dataRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> SelectAsync(int skip, int take, Func<T, bool> selector, DataRequest<T> dataRequest)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(DataRequest<T> dataRequest)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(params T[] items)
        {
            throw new NotImplementedException();
        }
    }
}
