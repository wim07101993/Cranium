﻿using Cranium.WPF.Helpers.Data.Mongo;

namespace Cranium.WPF.Data.Game
{
    public class MongoGameService : AMongoModelService<Game>, IGameDataService
    {
        private const string CollectionName = "games";

        public MongoGameService(IMongoDataServiceSettings settings) 
            : base(settings, CollectionName)
        {
        }
    }
}