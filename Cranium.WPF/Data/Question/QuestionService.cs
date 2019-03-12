using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Data.Mongo;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Question
{
    public class QuestionService : AMongoModelService<Question>, IQuestionService
    {
        private const string CollectionName = "questions";

        private readonly IFileService _fileService;


        public QuestionService(IMongoDataServiceSettings settings, IFileService fileService)
            : base(settings, CollectionName)
        {
            _fileService = fileService;
        }


        public async Task<byte[]> GetAttachmentAsync(Question question)
        {
            if (question.Attachment == default)
                return null;

            return await _fileService.GetOneAsync(question.Attachment);
        }

        public async Task<ObjectId> UpdateAttachment(
            Question question, Stream fileStream, string fileName, EAttachmentType attachmentType)
        {
            var oldQuestion = await GetOneAsync(question.Id);
            if (oldQuestion.Attachment != default)
                await _fileService.RemoveAsync(oldQuestion.Attachment);

            var newAttachmentId = await _fileService.CreateAsync(fileStream, fileName);
            question.Attachment = newAttachmentId;
            question.AttachmentType = attachmentType;

            await UpdateAsync(question);

            return newAttachmentId;
        }
    }
}