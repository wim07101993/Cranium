using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Data.Attachment;
using Cranium.WPF.Helpers.Data;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Question
{
    public interface IQuestionService : IModelService<Question>
    {
        Task<byte[]> GetAttachmentAsync(Question question);

        Task<ObjectId> UpdateAttachment(
            Question question, Stream fileStream, string fileName, EAttachmentType attachmentType);

        Task<ObjectId> UpdateAttachment(Question question, string filePath);
    }
}