using Data.Models;
using Data.Services.Files;

namespace Data.Services.Data.File
{
    public class QuestionTypeService : ADataService<QuestionType>
    {
        public QuestionTypeService(IFileService fileService, IFileLocationProvider fileLocationProvider) 
            : base(fileService, fileLocationProvider)
        {
        }
    }
}
