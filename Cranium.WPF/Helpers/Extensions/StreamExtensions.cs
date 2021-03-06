﻿using System.IO;
using System.Windows.Media.Imaging;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class StreamExtensions
    {
        public static BitmapImage ToImage(this Stream stream)
        {
            var image = new BitmapImage();
            stream.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = stream;
            image.EndInit();

            image.Freeze();
            return image;
        }

    }
}
