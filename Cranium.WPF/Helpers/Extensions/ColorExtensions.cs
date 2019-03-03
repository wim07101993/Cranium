using System.Windows.Media;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class ColorExtensions
    {
        public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color mColor)
            => System.Drawing.Color.FromArgb(mColor.A, mColor.R, mColor.G, mColor.B);

        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color dColor)
            => new System.Windows.Media.Color {A = dColor.A, R = dColor.R, G = dColor.G, B = dColor.B};

        public static SolidColorBrush ToSolidColorBrush(this System.Drawing.Color color)
            => color.ToMediaColor().ToSolidColorBrush();

        public static SolidColorBrush ToSolidColorBrush(this System.Windows.Media.Color color)
            => new SolidColorBrush(color);

        public static System.Drawing.Color ToDrawingColor(this SolidColorBrush brush)
            => brush.ToMediaColor().ToDrawingColor();

        public static System.Windows.Media.Color ToMediaColor(this SolidColorBrush brush)
            => brush.Color;
    }
}