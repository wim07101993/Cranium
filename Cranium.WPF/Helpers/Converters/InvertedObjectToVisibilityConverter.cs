﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cranium.WPF.Helpers.Converters
{
    public class InvertedObjectToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => value == null
                ? Visibility.Visible
                : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => throw new NotImplementedException();
    }
}
