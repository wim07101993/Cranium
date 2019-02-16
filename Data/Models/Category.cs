using Data.Models.Bases;

namespace Data.Models
{
    public class Category : AWithId
    {
        #region FIELDS

        private string _name;
        private string _description;
        private byte[] _image;

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

        #endregion PROPERTIES
    }
}