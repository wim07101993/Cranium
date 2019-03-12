using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Cranium.WPF.Helpers.Data
{
    /// <summary>
    /// Interface that describes a class that provides basic CRUD operations for a database.
    /// </summary>
    /// <typeparam name="T">Type of the item to preform the CRUD operation on.</typeparam>
    public interface IModelService<T>
        where T : IWithId
    {
        Task<T> CreateAsync(T item);
        
        Task<T> GetOneAsync(ObjectId id);

        Task<IList<T>> GetAsync();

        Task UpdateAsync(T newItem);

        Task RemoveAsync(ObjectId id);
    }
}
