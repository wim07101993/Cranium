using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Data.Models.Bases;

namespace Data.Services.Data
{
    public interface IDataService<T> : IDisposable where T : IWithId
    {
        Task CreateAsync(T item);

        Task<T> GetAsync(long id);
        Task<IList<T>> GetAsync(int skip = 0, int take = -1, DataRequest<T> dataRequest = null);
        Task<IList<TOut>> SelectAsync<TOut>(Func<T, TOut> selector, int skip = 0, int take = -1, DataRequest<T> dataRequest = null);
        Task<int> GetCountAsync(DataRequest<T> dataRequest = null);

        Task UpdateAsync(T item);
        Task DeleteAsync(params T[] items);
    }
}