namespace Cranium.Data.Models
{
    public class Team : AWithId
    {
        #region FIELDS

        private string _name;
        private Color _color;
        #endregion FIELDS


        #region PROPERTIES

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        #endregion PROPERTIES
    }
}
