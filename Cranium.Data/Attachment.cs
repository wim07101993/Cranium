using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Cranium.Data
{
    public class Attachment : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("file"), JsonProperty("file")]
        public Guid File { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("content"), JsonProperty("content")]
        public string Content { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("attachmentType"), JsonProperty("attachmentType")]
        public EAttachmentType AttachmentType { get; set; }
    }
}
