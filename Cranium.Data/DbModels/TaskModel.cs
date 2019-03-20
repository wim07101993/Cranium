using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.DbModels
{
    public class TaskModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("value"), JsonProperty("value")]
        public string Value { get; set; }

        [BsonElement("tip"), JsonProperty("tip")]
        public string Tip { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("taskType"), JsonProperty("taskType")]
        public Guid TaskType { get; set; }

        [BsonElement("attachments"), JsonProperty("attachments")]
        public IList<Guid> Attachments { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("answers"), JsonProperty("answers")]
        public IList<AnswerModel> Answers { get; set; }
    }
}
