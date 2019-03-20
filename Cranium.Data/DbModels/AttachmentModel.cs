using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Cranium.Data.DbModels
{
    public class AttachmentModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("file"), JsonProperty("file")]
        public string File { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("value"), JsonProperty("value")]
        public string Value { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("attachmentType"), JsonProperty("attachmentType")]
        public EAttachmentType AttachmentType { get; set; }
    }
}
