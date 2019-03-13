using System.ComponentModel;
using System.Windows.Media;
using Cranium.WPF.Helpers;
using Cranium.WPF.Helpers.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Color = Cranium.WPF.Helpers.Color;

namespace Cranium.WPF.Data.Category
{
    public class Category : AWithId
    {
        private string _name;
        private string _description;
        private ObjectId _image;
        private Color _color = new Color{ BaseColor = Colors.Transparent };
        private bool _isSpecial;


        public Category()
        {
            Color.PropertyChanged += OnColorPropertyChanged;
        }


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

        [BsonElement("image")]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        [BsonElement("color")]
        public Color Color
        {
            get => _color;
            set
            {
                
                if (Equals(_color, value))
                    return;

                if (_color != null)
                    Color.PropertyChanged -= OnColorPropertyChanged;

                SetProperty(ref _color, value);

                if (_color != null)
                    Color.PropertyChanged += OnColorPropertyChanged;

                RaisePropertyChanged(nameof(Color));
            }
        }

        [BsonElement("isSpecial")]
        public bool IsSpecial
        {
            get => _isSpecial;
            set => SetProperty(ref _isSpecial, value);
        }


        private void OnColorPropertyChanged(object sender, PropertyChangedEventArgs e) 
            => RaisePropertyChanged(nameof(Color));

        public override string ToString() => Name;
    }
}