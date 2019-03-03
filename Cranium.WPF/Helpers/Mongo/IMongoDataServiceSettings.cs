namespace Cranium.WPF.Helpers.Mongo
{
    public interface IMongoDataServiceSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
