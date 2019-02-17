using Data.Models;
using Data.Services.Files;

namespace Data.Services.Data.File
{
    public class QuestionService : ADataService<Question>
    {
        public QuestionService(IFileService fileService, IFileLocationProvider fileLocationProvider) 
            : base(fileService, fileLocationProvider)
        {
        }
    }
}
