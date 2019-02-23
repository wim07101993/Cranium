using Cranium.WPF.Models;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class QuestionService : AMongoDataService<Question>, IQuestionService
    {
        public QuestionService(IMongoDataServiceSettings settings) : base(settings, "questions")
        {
        }
    }
}
