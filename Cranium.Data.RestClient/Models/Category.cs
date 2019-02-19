using Cranium.Data.RestClient.Attributes;
using Cranium.Data.RestClient.Models.Bases;
using Newtonsoft.Json;
using System.Drawing;

namespace Cranium.Data.RestClient.Models
{
    [HasController("Categories", true)]
    public class Category : AWithId
    {
        private string _name;
        private string _description;
        private byte[] _image;
        private Color _color = Color.Blue;


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public byte[] Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        [JsonConverter(typeof(JsonColorConverter))]
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }
}