using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cranium.WPF.Helpers.Converters
{
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b && b
                ? Visibility.Collapsed
                : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility visibility && visibility == Visibility.Collapsed;
    }
}