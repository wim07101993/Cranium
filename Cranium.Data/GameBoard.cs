using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;

namespace Cranium.Data
{
    public class GameBoard : List<Guid>, IWithId
    {
        [BsonId]
        [BsonRequired, JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
