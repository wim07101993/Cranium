using Cranium.WPF.Models;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class CategoryService : AMongoModelService<Category>, ICategoryService
    {
        public CategoryService(IMongoDataServiceSettings settings) : base(settings,  "categories")
        {
        }
    }
}