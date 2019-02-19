using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Cranium.WPF.Extensions;

namespace Cranium.WPF.Converters
{
    public class BytesToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is byte[] imageData
                ? imageData.ToImage()
                : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is BitmapImage bitmap
                ? bitmap.ToBytes()
                : null;
        }
    }
}