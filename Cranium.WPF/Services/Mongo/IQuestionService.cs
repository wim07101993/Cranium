using System.IO;
using System.Threading.Tasks;
using Cranium.WPF.Models;
using MongoDB.Bson;

namespace Cranium.WPF.Services.Mongo
{
    public interface IQuestionService : IModelService<Question>
    {
        Task<byte[]> GetAttachmentAsync(ObjectId questionId);

        Task<ObjectId> UpdateAttachment(
            ObjectId categoryId, Stream fileStream, string fileName, EAttachmentType attachmentType);
    }
}