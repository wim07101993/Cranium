using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Cranium.WPF.Game.Player;
using Shared.Extensions;

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

        public static void AutoUpdateCollection<TCollection, TCollectionToUpdate>(
            this INotifyCollectionChanged collection,
            IList<TCollectionToUpdate> collectionToUpdate,
            Func<TCollection, TCollectionToUpdate> itemConversion,
            Func<TCollection, TCollectionToUpdate, bool> comparer)
        {
            collection.CollectionChanged += (sender, e) =>
            {
                var addedPlayers = e.NewItems.Cast<TCollection>().ToList();
                var removedPlayers = e.OldItems.Cast<TCollection>().ToList();

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        collectionToUpdate.Add(addedPlayers.Select(itemConversion));
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        collectionToUpdate.RemoveWhere(tToUpdate => removedPlayers.Any(t => comparer(t, tToUpdate)));
                        break;
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                        collectionToUpdate.RemoveWhere(tToUpdate => removedPlayers.Any(t => comparer(t, tToUpdate)));
                        collectionToUpdate.Add(addedPlayers.Select(itemConversion));
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        collectionToUpdate.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }
    }
}