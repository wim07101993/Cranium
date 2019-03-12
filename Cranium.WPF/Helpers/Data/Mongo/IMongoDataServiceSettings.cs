namespace Cranium.WPF.Helpers.Data.Mongo
{
    public interface IMongoDataServiceSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
