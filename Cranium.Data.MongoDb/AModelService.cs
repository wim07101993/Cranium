using Cranium.Data.Exceptions;
using MongoDB.Driver;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cranium.Data.MongoDb
{
    public abstract class AModelService<TDb, T> : IModelService<T>
       where T : IWithId
       where TDb : IWithId
    {
        #region CONSTRUCTORS

        protected AModelService(IDataServiceSettings settings, string collectionName)
        {
            MongoCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<TDb>(collectionName);
        }

        #endregion CONSTRCUTORS


        #region PROPERTIES

        public IMongoCollection<TDb> MongoCollection { get; }

        #endregion PROPERTIES


        #region METHDOS

        public abstract Task<T> CreateAsync(T item);
        protected virtual async Task<TDb> CreateInDbAsync(TDb item)
        {
            try
            {
                return await CreateInDbAsync(item, true);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Create, e);
            }
        }

        protected abstract Task<T> CreateAsync(T item, bool generateNewId);
        protected virtual async Task<TDb> CreateInDbAsync(TDb item, bool generateNewId)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (item == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(item));

            if (generateNewId)
                item.Id = Guid.NewGuid();

            await MongoCollection.InsertOneAsync(item);
            return item;
        }

        public abstract Task<IList<T>> GetAsync();
        protected virtual async Task<IList<TDb>> GetFromDbAsync()
        {
            try
            {
                return await MongoCollection
                          .Find(FilterDefinition<TDb>.Empty)
                          .ToListAsync();
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Read, e);
            }
        }

        public abstract Task<T> GetOneAsync(Guid id);
        protected virtual async Task<TDb> GetOneFromDbAsync(Guid id)
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

        public abstract Task UpdateAsync(T newItem);
        public virtual async Task UpdateInDbAsync(TDb newItem)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newItem == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
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

        public abstract Task RemoveAsync(Guid id);
        public virtual async Task RemoveFromDbAsync(Guid id)
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
