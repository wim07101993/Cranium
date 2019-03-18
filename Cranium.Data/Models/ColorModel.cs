using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Cranium.Data.Models
{
    public class ColorModel
    {
        [BsonElement("a"), JsonProperty("a")]
        public byte A { get; set; } = 255;

        [BsonElement("r"), JsonProperty("r")]
        public byte R { get; set; }

        [BsonElement("g"), JsonProperty("g")]
        public byte G { get; set; }

        [BsonElement("b"), JsonProperty("b")]
        public byte B { get; set; }
    }
}
