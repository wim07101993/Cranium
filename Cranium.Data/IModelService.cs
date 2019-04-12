using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cranium.Data
{
    public interface IModelService<T>
       where T : IWithId
    {
        Task<T> CreateAsync(T model);

        Task<T> GetOneAsync(Guid id);

        Task<IList<T>> GetAsync();

        Task UpdateAsync(T newModel);
        Task UpdateAsync(IEnumerable<T> newModels);
        Task UpdateOrCreateAsync(T newModel);
        Task UpdateOrCreateAsync(IEnumerable<T> newModels);

        Task RemoveAsync(Guid id);
    }
}
