using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Cranium.WPF.Converters
{
    public class DrawingColorToMediaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is System.Drawing.Color dColor
                ? new Color {A = dColor.A, G = dColor.G, B = dColor.B, R = dColor.R}
                : default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Color mC
                ? System.Drawing.Color.FromArgb(mC.A, mC.R, mC.G, mC.B)
                : default;
        }
    }
}
