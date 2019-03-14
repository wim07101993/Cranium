using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Cranium.Data
{
    public class Team : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        
        [BsonElement("color"), JsonProperty("color")]
        public Color Color { get; set; }
    }
}
