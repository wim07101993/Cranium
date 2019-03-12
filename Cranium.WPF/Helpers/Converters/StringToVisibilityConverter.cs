using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cranium.WPF.Helpers.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case string s:
                    return string.IsNullOrWhiteSpace(s)
                         ? Visibility.Collapsed
                         : Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
