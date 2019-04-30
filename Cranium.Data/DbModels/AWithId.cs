using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Shared;

namespace Cranium.Data.DbModels
{
    public class AWithId : IWithId
    {
        [BsonId]
        [BsonRequired, JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
