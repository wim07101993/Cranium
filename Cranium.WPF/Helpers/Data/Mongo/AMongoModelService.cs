using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.WPF.Helpers.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cranium.WPF.Helpers.Data.Mongo
{
    /// <inheritdoc />
    /// <summary>
    /// An abstract class to preform the basic CRUD operations on a Mongo Database.
    /// </summary>
    /// <typeparam name="T">Type of the entities to preform the CRUD operations on</typeparam>
    public abstract class AMongoModelService<T> : IModelService<T>
        where T : IWithId
    {
        #region CONSTRUCTORS

        protected AMongoModelService(IMongoDataServiceSettings settings, string collectionName)
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
            => await CreateAsync(item, true);

        protected virtual async Task<T> CreateAsync(T item, bool generateNewId)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (generateNewId)
                item.Id = ObjectId.GenerateNewId();

            try
            {
                await MongoCollection.InsertOneAsync(item);
                return item;
            }
            catch (Exception e)
            {
                throw new DatabaseException(EDatabaseMethod.Create, e);
            }
        }

        public virtual async Task<IList<T>> GetAsync()
            => await MongoCollection
                .Find(FilterDefinition<T>.Empty)
                .ToListAsync();

        public virtual async Task<T> GetOneAsync(ObjectId id)
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
                throw new DatabaseException(EDatabaseMethod.Read, e);
            }
        }

        public virtual async Task UpdateAsync(T newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));

            var replaceOneResult = await MongoCollection.ReplaceOneAsync(x => x.Id == newItem.Id, newItem);

            if (!replaceOneResult.IsAcknowledged)
                throw new DatabaseException(EDatabaseMethod.Replace);
            if (replaceOneResult.MatchedCount <= 0)
                throw new NotFoundException<T>(nameof(IWithId.Id), newItem.Id.ToString());
        }

        public virtual async Task RemoveAsync(ObjectId id)
        {
            var deleteResult = await MongoCollection.DeleteOneAsync(x => x.Id == id);

            if (!deleteResult.IsAcknowledged)
                throw new DatabaseException(EDatabaseMethod.Delete);
            if (deleteResult.DeletedCount <= 0)
                throw new NotFoundException<T>();
        }

        #endregion METHODS
    }
}