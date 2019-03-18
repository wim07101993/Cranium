using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;

namespace Cranium.Data.Models
{
    public class GameBoardModel : List<Guid>, IWithId
    {
        [BsonId]
        [BsonRequired, JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
