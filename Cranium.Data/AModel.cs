using System;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Shared;

namespace Cranium.Data
{
    public class AModel : IWithId
    {
        [BsonId]
        [BsonRequired, JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
