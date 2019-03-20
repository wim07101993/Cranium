namespace Cranium.Data.Models
{
    public class Category : AWithId
    {
        #region FIELDS

        private string _name;
        private string _description;

        private byte[] _image;
        private bool _fetchedImage;

        private Color _color;
        private bool _isSpecial;

        #endregion FIELDS


        #region PROPERTIES

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

        public bool FetchedImage
        {
            get => _fetchedImage;
            internal set => SetProperty(ref _fetchedImage, value);
        }

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public bool IsSpecial
        {
            get => _isSpecial;
            set => SetProperty(ref _isSpecial, value);
        }

        #endregion PROPERTIES
    }
}
