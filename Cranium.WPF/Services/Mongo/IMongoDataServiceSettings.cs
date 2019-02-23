namespace Cranium.WPF.Services.Mongo
{
    public interface IMongoDataServiceSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
