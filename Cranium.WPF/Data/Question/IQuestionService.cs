using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Data.Files;
using Cranium.WPF.Helpers.Mongo;
using MongoDB.Bson;

namespace Cranium.WPF.Data.Question
{
    public interface IQuestionService : IModelService<Question>
    {
        Task<byte[]> GetAttachmentAsync(ObjectId questionId);

        Task<ObjectId> UpdateAttachment(
            ObjectId categoryId, Stream fileStream, string fileName, EAttachmentType attachmentType);
    }
}