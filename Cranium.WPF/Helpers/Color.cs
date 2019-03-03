using System;
using MongoDB.Bson.Serialization.Attributes;
using Prism.Mvvm;

namespace Cranium.WPF.Helpers
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Color : BindableBase, IEquatable<Color>, IEquatable<System.Windows.Media.Color>,
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
        IEquatable<System.Drawing.Color>
    {
        private System.Windows.Media.Color _baseColor;

        [BsonIgnore]
        public System.Windows.Media.Color BaseColor
        {
            get => _baseColor;
            set
            {
                if (!SetProperty(ref _baseColor, value))
                    return;

                RaisePropertyChanged(nameof(A));
                RaisePropertyChanged(nameof(R));
                RaisePropertyChanged(nameof(G));
                RaisePropertyChanged(nameof(B));
            }
        }

        [BsonElement("a")]
        public byte A
        {
            get => BaseColor.A;
            set => BaseColor = new System.Windows.Media.Color
            {
                A = value,
                R = BaseColor.R,
                G = BaseColor.G,
                B = BaseColor.B
            };
        }

        [BsonElement("r")]
        public byte R
        {
            get => BaseColor.R;
            set => BaseColor = new System.Windows.Media.Color
            {
                A = BaseColor.A,
                R = value,
                G = BaseColor.G,
                B = BaseColor.B
            };
        }

        [BsonElement("g")]
        public byte G
        {
            get => BaseColor.G;
            set => BaseColor = new System.Windows.Media.Color
            {
                A = BaseColor.A,
                R = BaseColor.R,
                G = value,
                B = BaseColor.B
            };
        }

        [BsonElement("b")]
        public byte B
        {
            get => BaseColor.B;
            set => BaseColor = new System.Windows.Media.Color
            {
                A = BaseColor.A,
                R = BaseColor.R,
                G = BaseColor.G,
                B = value
            };
        }

        public bool Equals(Color other) => other != null && A == other.A && B == other.B && G == other.G;

        public bool Equals(System.Windows.Media.Color other) => A == other.A && B == other.B && G == other.G;

        public bool Equals(System.Drawing.Color other) => A == other.A && B == other.B && G == other.G;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Color c && Equals(c) ||
                   obj is System.Drawing.Color bc && Equals(bc) ||
                   obj is System.Windows.Media.Color mc && Equals(mc);
        }

        public static bool operator ==(Color first, Color second)
            => first?.Equals(second) == true || ReferenceEquals(first, null) && ReferenceEquals(second, null);

        public static bool operator !=(Color first, Color second) => !(first == second);
    }
}