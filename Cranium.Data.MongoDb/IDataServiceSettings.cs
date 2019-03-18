namespace Cranium.Data.MongoDb
{
    public interface IDataServiceSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
