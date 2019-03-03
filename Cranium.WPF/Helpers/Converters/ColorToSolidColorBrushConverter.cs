using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Cranium.WPF.Helpers.Extensions;

namespace Cranium.WPF.Helpers.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case System.Drawing.Color dw:
                    return dw.ToSolidColorBrush();
                case System.Windows.Media.Color mc:
                    return mc.ToSolidColorBrush();
                case Helpers.Color c:
                    return new SolidColorBrush(c.BaseColor);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush brush))
                return Activator.CreateInstance(targetType);

            if (targetType == typeof(System.Drawing.Color))
                return brush.ToDrawingColor();
            if (targetType == typeof(System.Windows.Media.Color))
                return brush.ToMediaColor();
            if (targetType == typeof(Helpers.Color))
                return new Helpers.Color {BaseColor = brush.Color};

            return Activator.CreateInstance(targetType);
        }
    }
}