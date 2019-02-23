using Cranium.WPF.Models;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class QuestionTypeService : AMongoDataService<QuestionType>, IQuestionTypeService
    {
        public QuestionTypeService(IMongoDataServiceSettings settings) : base(settings, "questionTypes")
        {
        }
    }
}
