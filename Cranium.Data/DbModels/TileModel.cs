using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.DbModels
{
    public class TileModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("category"), JsonProperty("category")]
        public Guid Category { get; set; }

        [BsonElement("teams"), JsonProperty("teams")]
        public IList<Guid> Teams { get; set; }
    }
}