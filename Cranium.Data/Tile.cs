using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data
{
    public class Tile : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("category"), JsonProperty("category")]
        public Guid Category { get; set; }

        [BsonElement("players"), JsonProperty("players")]
        public List<Guid> Players { get; set; }
    }
}