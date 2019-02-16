using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Data.Models.Bases;

namespace Data.Services
{
    public interface IDataService<T> where T : IWithId
    {
        Task<bool> CreateAsync(T item);

        Task<T> GetAsync(long id);
        Task<IList<T>> GetAsync(int skip, int take, DataRequest<T> dataRequest);
        Task<IList<T>> SelectAsync(int skip, int take, Func<T, bool> selector, DataRequest<T> dataRequest);
        Task<int> GetCountAsync(DataRequest<T> dataRequest);

        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(params T[] items);
    }
}
