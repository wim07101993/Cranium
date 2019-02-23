namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class MongoDataServiceSettings : IMongoDataServiceSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
