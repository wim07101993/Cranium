using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shared;

namespace Cranium.Data.Services
{
    public interface IModelService<T> where T : IWithId
    {
        Task<T> CreateAsync(T item);

        Task<T> GetOneAsync(Guid id);
        Task<T> GetPropertyAsync(Guid id, string propertyName);

        Task<IQueryable<T>> GetQueryable();
        Task<IList<T>> GetAsync();

        Task UpdateAsync(T newItem);
        Task UpdatePropertyAsync(T newItem);
        
        Task RemoveAsync(ObjectId id);
    }
}
