using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using MongoDB.Bson;

namespace Cranium.WPF.Services.Mongo.Implementations
{
    public class QuestionService : AMongoModelService<Question>, IQuestionService
    {
        private readonly IFileService _fileService;


        public QuestionService(IMongoDataServiceSettings settings, IFileService fileService)
            : base(settings, "questions")
        {
            _fileService = fileService;
        }


        public async Task<byte[]> GetAttachmentAsync(ObjectId questionId)
        {
            var attachmentId = await GetPropertyAsync(questionId, x => x.Attachment);
            if (attachmentId == default)
                return null;

            return await _fileService.GetOneAsync(attachmentId);
        }

        public async Task<ObjectId> UpdateAttachment(
            ObjectId categoryId, Stream fileStream, string fileName, EAttachmentType attachmentType)
        {
            var oldAttachmentId = await GetPropertyAsync(categoryId, x => x.Attachment);
            if (oldAttachmentId != default)
                await _fileService.RemoveAsync(oldAttachmentId);

            var newAttachmentId = await _fileService.CreateAsync(fileStream, fileName);

            await UpdatePropertyAsync(categoryId, x => x.Attachment, newAttachmentId);
            await UpdatePropertyAsync(categoryId, x => x.AttachmentType, attachmentType);

            return newAttachmentId;
        }
    }
}