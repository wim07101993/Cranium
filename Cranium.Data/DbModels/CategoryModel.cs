using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Cranium.Data.DbModels
{
    public class CategoryModel : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("description"), JsonProperty("description")]
        public string Description { get; set; }

        [BsonElement("image"), JsonProperty("image")]
        public Guid Image { get; set; }

        [BsonElement("color"), JsonProperty("color")]
        public ColorModel Color { get; set; }

        [BsonElement("isSpecial"), JsonProperty("isSpecial")]
        public bool IsSpecial { get; set; }
    }
}
