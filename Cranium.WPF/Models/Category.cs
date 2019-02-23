using System.Windows.Media;
using Cranium.WPF.Models.Bases;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Models
{
    public class Category : AWithId
    {
        private string _name;
        private string _description;
        private ObjectId _image;
        private Color _color;

        [BsonRequired]
        [BsonElement("name")]
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        [BsonElement("description")]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        [BsonIgnore]
        [BsonElement("image")]
        public ObjectId Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        [BsonElement("color")]
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }
}