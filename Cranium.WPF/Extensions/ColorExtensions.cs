using System.Windows.Media;

namespace Cranium.WPF.Extensions
{
    public static class ColorExtensions
    {
        public static System.Drawing.Color ToDrawingColor(this Color mColor)
            => System.Drawing.Color.FromArgb(mColor.A, mColor.R, mColor.G, mColor.B);

        public static Color ToMediaColor(this System.Drawing.Color dColor)
            => new Color {A = dColor.A, R = dColor.R, G = dColor.G, B = dColor.B};

        public static SolidColorBrush ToSolidColorBrush(this System.Drawing.Color color)
            => color.ToMediaColor().ToSolidColorBrush();

        public static SolidColorBrush ToSolidColorBrush(this Color color)
            => new SolidColorBrush(color);

        public static System.Drawing.Color ToDrawingColor(this SolidColorBrush brush)
            => brush.ToMediaColor().ToDrawingColor();

        public static Color ToMediaColor(this SolidColorBrush brush)
            => brush.Color;
    }
}