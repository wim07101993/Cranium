using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.Models
{
    public class TaskModel : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("value"), JsonProperty("value")]
        public string Value { get; set; }

        [BsonElement("tip"), JsonProperty("tip")]
        public string Tip { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("questionType"), JsonProperty("questionType")]
        public Guid QuestionType { get; set; }

        [BsonElement("attachments"), JsonProperty("attachments")]
        public List<Guid> Attachments { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("answers"), JsonProperty("answers")]
        public List<AnswerModel> Answers { get; set; }
    }
}
