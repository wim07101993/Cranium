using Cranium.WPF.Helpers.Data.Mongo;

namespace Cranium.WPF.Data.QuestionType
{
    public class MongoQuestionTypeService : AMongoModelService<QuestionType>, IQuestionTypeService
    {
        public MongoQuestionTypeService(IMongoDataServiceSettings settings) : base(settings, "questionTypes")
        {
        }
    }
}
