using Cranium.WPF.Models;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class QuestionTypeService : AMongoModelService<QuestionType>, IQuestionTypeService
    {
        public QuestionTypeService(IMongoDataServiceSettings settings) : base(settings, "questionTypes")
        {
        }
    }
}
