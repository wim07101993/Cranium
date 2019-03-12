﻿using Cranium.WPF.Helpers.Data.Mongo;

namespace Cranium.WPF.Game
{
    public class MongoGameService : AMongoModelService<Game>, IMongoGameService
    {
        private const string CollectionName = "games";

        public MongoGameService(IMongoDataServiceSettings settings) 
            : base(settings, CollectionName)
        {
        }
    }
}