using Cranium.Data.Exceptions;
using MongoDB.Driver;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cranium.Data.MongoDb
{
    public abstract class AModelService<T> : IModelService<T>
       where T : IWithId
    {
        #region CONSTRUCTORS

        protected AModelService(IDataServiceSettings settings, string collectionName)
        {
            MongoCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<T>(collectionName);
        }

        #endregion CONSTRCUTORS


        #region PROPERTIES

        public IMongoCollection<T> MongoCollection { get; }

        #endregion PROPERTIES


        #region METHDOS

        public virtual async Task<T> CreateAsync(T item)
        {
            try
            {
                return await CreateAsync(item, true);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Create, e);
            }
        }

        protected virtual async Task<T> CreateAsync(T item, bool generateNewId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (generateNewId)
                item.Id = Guid.NewGuid();

            await MongoCollection.InsertOneAsync(item);
            return item;
        }

        public virtual async Task<IList<T>> GetAsync()
        {
            try
            {
                return await MongoCollection
                          .Find(FilterDefinition<T>.Empty)
                          .ToListAsync();
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Read, e);
            }
        }

        public virtual async Task<T> GetOneAsync(Guid id)
        {
            try
            {
                var find = MongoCollection.Find(x => x.Id == id);

                if (!find.Any())
                    throw new NotFoundException<T>();

                return await find
                    .FirstOrDefaultAsync();
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Read, e);
            }
        }

        public virtual async Task UpdateAsync(T newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));

            ReplaceOneResult result = null;

            try
            {
                result = await MongoCollection.ReplaceOneAsync(x => x.Id == newItem.Id, newItem);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Update, e);
            }

            if (!result.IsAcknowledged)
                throw new DataException(ECrudMethod.Replace);
            if (result.MatchedCount <= 0)
                throw new NotFoundException<T>(nameof(IWithId.Id), newItem.Id.ToString());
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            DeleteResult result = null;

            try
            {
                result = await MongoCollection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Delete, e);
            }

            if (!result.IsAcknowledged)
                throw new DataException(ECrudMethod.Delete);
            if (result.DeletedCount <= 0)
                throw new NotFoundException<T>();
        }

        #endregion METHODS
    }
}
