using Cranium.Data.DbModels;
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

        protected AModelService(IDataServiceSettings settings, string collectionName = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                collectionName = $"{typeof(T).Name}s";

            MongoCollection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<TDb>(collectionName);
        }

        #endregion CONSTRCUTORS


        #region PROPERTIES

        public IMongoCollection<TDb> MongoCollection { get; }

        #endregion PROPERTIES


        #region METHDOS

        #region create

        public virtual async Task<T> CreateAsync(T model) 
            => await CreateAsync(model, false);

        protected virtual async Task<T> CreateAsync(T model, bool generateNewId)
        {
            var dbItem = await ModelToDbModel(model);
            dbItem = await CreateInDbAsync(dbItem, generateNewId);
            return await DbModelToModelAsync(dbItem);
        }

        protected virtual async Task<TDb> CreateInDbAsync(TDb model, bool generateNewId)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (model == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(model));

            if (generateNewId)
                model.Id = Guid.NewGuid();

            await MongoCollection.InsertOneAsync(model);
            return model;
        }

        #endregion create


        #region get

        public virtual async Task<IList<T>> GetAsync()
        {
            var dbModels = await GetFromDbAsync();

            var models = new List<T>(dbModels.Count);
            foreach (var dbTask in dbModels)
                models.Add(await DbModelToModelAsync(dbTask));

            return models;
        }

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

        public virtual async Task<T> GetOneAsync(Guid id)
        {
            var dbModel = await GetOneFromDbAsync(id);
            return await DbModelToModelAsync(dbModel);
        }

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

        #endregion get


        #region update

        public virtual async Task UpdateAsync(T newModel)
        {
            var dbModel = await ModelToDbModel(newModel);
            await UpdateInDbAsync(dbModel);
        }

        public virtual async Task UpdateAsync(IEnumerable<T> newModels)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModels == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModels));

            foreach (var newModel in newModels)
                await UpdateAsync(newModel);
        }

        public virtual async Task UpdateOrCreateAsync(T newModel)
        {
            var dbModel = await ModelToDbModel(newModel);
            await UpdateOrCreateInDbAsync(dbModel);
        }

        public virtual async Task UpdateOrCreateAsync(IEnumerable<T> newModels)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModels == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModels));

            foreach (var newModel in newModels)
                await UpdateOrCreateAsync(newModel);
        }


        public virtual async Task UpdateInDbAsync(TDb newModel)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModel == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModel));

            ReplaceOneResult result = null;

            try
            {
                result = await MongoCollection.ReplaceOneAsync(x => x.Id == newModel.Id, newModel);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Update, e);
            }

            if (!result.IsAcknowledged)
                throw new DataException(ECrudMethod.Replace);
            if (result.MatchedCount <= 0)
                throw new NotFoundException<T>(nameof(IWithId.Id), newModel.Id.ToString());
        }

        public virtual async Task UpdateInDbAsync(IEnumerable<TDb> newModels)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModels == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModels));

            foreach (var newModel in newModels)
                await UpdateInDbAsync(newModel);
        }

        public virtual async Task UpdateOrCreateInDbAsync(TDb newModel, bool generateNewId)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModel == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModel));

            ReplaceOneResult result = null;

            try
            {
                result = await MongoCollection.ReplaceOneAsync(x => x.Id == newModel.Id, newModel);
            }
            catch (Exception e)
            {
                throw new DataException(ECrudMethod.Update, e);
            }

            if (result.MatchedCount <= 0)
                await CreateInDbAsync(newModel, true);
            else if (!result.IsAcknowledged)
                throw new DataException(ECrudMethod.Replace);
        }

        public virtual async Task UpdateOrCreateInDbAsync(IEnumerable<TDb> newModels)
        {
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (newModels == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
                throw new ArgumentNullException(nameof(newModels));

            foreach (var newModel in newModels)
                await UpdateOrCreateInDbAsync(newModel);
        }

        #endregion update


        #region delete

        public override async Task RemoveAsync(Guid id)
        {
            await RemoveFromDbAsync(id);
        }
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

        #endregion delete


        public abstract Task<T> DbModelToModelAsync(TDb dbModel);
        public abstract Task<TDb> ModelToDbModel(T model);

        #endregion METHODS
    }
}
