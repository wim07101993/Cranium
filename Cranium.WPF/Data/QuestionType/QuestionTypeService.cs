using Cranium.WPF.Helpers.Data.Mongo;

namespace Cranium.WPF.Data.QuestionType
{
    public class QuestionTypeService : AMongoModelService<QuestionType>, IQuestionTypeService
    {
        public QuestionTypeService(IMongoDataServiceSettings settings) : base(settings, "questionTypes")
        {
        }
    }
}
