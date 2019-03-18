using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Cranium.Data.Models
{
    public class AttachmentModel : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("file"), JsonProperty("file")]
        public Guid File { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("value"), JsonProperty("value")]
        public string Value { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("attachmentType"), JsonProperty("attachmentType")]
        public EAttachmentType AttachmentType { get; set; }
    }
}
