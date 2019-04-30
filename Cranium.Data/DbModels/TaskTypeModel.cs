using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Cranium.Data.DbModels
{
    public class TaskTypeModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("explanation"), JsonProperty("explanation")]
        public string Explanation { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("category"), JsonProperty("category")]
        public Guid Category { get; set; }
    }
}
