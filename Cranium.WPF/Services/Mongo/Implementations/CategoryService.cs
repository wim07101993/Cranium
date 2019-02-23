using Cranium.WPF.Models;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class CategoryService : AMongoDataService<Category>, ICategoryService
    {
        public CategoryService(IMongoDataServiceSettings settings) : base(settings,  "categories")
        {
        }
    }
}