using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Data.File;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Question
{
    public class FileQuestionService : AFileModelService<Question>, IQuestionService
    {
        private readonly FileAttachmentService _fileService;

        public FileQuestionService(FileAttachmentService fileService)
        {
            _fileService = fileService;
        }


        public async Task<byte[]> GetAttachmentAsync(Question question)
        {
            return await _fileService.GetOneAsync(question.Attachment);
        }

        public async Task<ObjectId> UpdateAttachment(Question question, Stream fileStream, string fileName, EAttachmentType attachmentType)
        {
            var oldQuestion = await GetOneAsync(question.Id);
            if (oldQuestion.Attachment != default)
                await _fileService.RemoveAsync(oldQuestion.Attachment);

            var newAttachmentId = await _fileService.CreateAsync(fileStream, fileName);
            question.Attachment = newAttachmentId;
            await UpdateAsync(question);

            return newAttachmentId;
        }

        public async Task<ObjectId> UpdateAttachment(Question question, string filePath)
        {
            var oldQuestion = await GetOneAsync(question.Id);
            if (oldQuestion.Attachment != default)
                await _fileService.RemoveAsync(oldQuestion.Attachment);

            var newAttachmentId = await _fileService.CreateAsync(filePath);
            question.Attachment = newAttachmentId;
            await UpdateAsync(question);

            return newAttachmentId;
        }
    }
}
