using Cranium.Data.ViewModels.Models.Bases;

namespace Cranium.Data.ViewModels.Models
{
    public class Category : AWithId
    {
        private string _name;
        private string _description;
        private byte[] _image;


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
    }
}