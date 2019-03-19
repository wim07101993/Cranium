using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.DbModels
{
    public class AnswerModel : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("isCorrect"), JsonProperty("isCorrect")]
        public bool IsCorrect { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("value"), JsonProperty("value")]
        public string Value { get; set; }

        [BsonElement("info"), JsonProperty("info")]
        public string Info { get; set; }

        [BsonElement("attachments"), JsonProperty("attachments")]
        public List<Guid> Attachments { get; set; }
    }
}
