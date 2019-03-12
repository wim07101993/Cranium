namespace Cranium.WPF.Helpers.Data.Mongo
{
    public class MongoDataServiceSettings : IMongoDataServiceSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
