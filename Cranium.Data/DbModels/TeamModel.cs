using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Cranium.Data.DbModels
{
    public class TeamModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        
        [BsonElement("color"), JsonProperty("color")]
        public ColorModel Color { get; set; }
    }
}
