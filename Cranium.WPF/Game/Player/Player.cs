using Cranium.WPF.Helpers;

namespace Cranium.WPF.Game.Player
{
    public class Player : AWithId
    {
        private string _name;
        private Color _color;


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
    }
}
