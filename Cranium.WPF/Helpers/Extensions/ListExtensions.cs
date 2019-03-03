﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Cranium.WPF.Helpers.Extensions
{
    public static class ListExtensions
    {
        public static BitmapImage ToImage(this byte[] bs)
        {
            if (bs.Length == 0)
                return null;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(bs))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();
            return image;
        }

        public static TList Add<TList, T>(this TList list, IEnumerable<T> itemsToAdd) where TList : ICollection<T>
        {
            foreach (var item in itemsToAdd)
                list.Add(item);
            return list;
        }

        public static int FindFirstIndex<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (var i = 0; i < list.Count; i++)
                if (predicate(list[i]))
                    return i;
            return -1;
        }
    }
}