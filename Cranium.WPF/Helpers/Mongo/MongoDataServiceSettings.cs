namespace Cranium.WPF.Helpers.Mongo
{
    public class MongoDataServiceSettings : IMongoDataServiceSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
