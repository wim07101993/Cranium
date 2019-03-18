using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cranium.Data
{
    public interface IModelService<T>
       where T : IWithId
    {
        Task<T> CreateAsync(T item);

        Task<T> GetOneAsync(Guid id);

        Task<IList<T>> GetAsync();

        Task UpdateAsync(T newItem);

        Task RemoveAsync(Guid id);
    }
}
