namespace Data.Services.Data.File
{
    public interface IFileLocationProvider
    {
        string GetLocationOfCollectionFile<T>();
    }
}
