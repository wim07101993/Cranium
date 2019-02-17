using Data.Models;
using Data.Services.Files;

namespace Data.Services.Data.File
{
    public class CategoryService : ADataService<Category>
    {
        public CategoryService(IFileService fileService, IFileLocationProvider fileLocationProvider)
            : base(fileService, fileLocationProvider)
        {
        }
    }
}