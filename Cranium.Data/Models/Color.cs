using Prism.Mvvm;

namespace Cranium.Data.Models
{
    public class Color : BindableBase
    {
        private byte _a;
        private byte _r;
        private byte _g;
        private byte _b;


        public byte A
        {
            get => _a;
            set => SetProperty(ref _a, value);
        }

        public byte R
        {
            get => _r;
            set => SetProperty(ref _r, value);
        }

        public byte G
        {
            get => _g;
            set => SetProperty(ref _g, value);
        }

        public byte B
        {
            get => _b;
            set => SetProperty(ref _b, value);
        }
    }
}
