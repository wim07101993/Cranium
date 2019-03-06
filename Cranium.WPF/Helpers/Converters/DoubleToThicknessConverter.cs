using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cranium.WPF.Helpers.Converters
{
    public class DoubleToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? d = value as double?;
            if (d == null)
                return new Thickness();
            return new Thickness((double)d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = value as Thickness?;
            if (t == null)
                return 0;
            return ((Thickness)t).Left;
        }
    }
}
