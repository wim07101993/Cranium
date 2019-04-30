namespace Cranium.Data.MongoDb
{
    public class DataServiceSettings : IDataServiceSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
