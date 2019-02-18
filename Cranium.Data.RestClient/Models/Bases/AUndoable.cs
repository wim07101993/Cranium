using Newtonsoft.Json;
using Prism.Mvvm;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cranium.Data.RestClient.Models.Bases
{
    public abstract class AUndoable : BindableBase
    {
        #region FIELDS

        #endregion FIELDS


        #region PROPERTIES

        [JsonIgnore]
        public virtual IReadOnlyCollection<string> ChangedProperties
            => new ReadOnlyCollection<string>(OldValues.Keys.ToList());

        [JsonIgnore]
        public virtual Dictionary<string, IList<object>> OldValues { get; } = new Dictionary<string, IList<object>>();

        #endregion PROPERTIES


        #region METHODS

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value) || string.IsNullOrWhiteSpace(propertyName))
                return false;

            AddValueToOldValueDictionary(propertyName, value);
            storage = value;

            RaisePropertyChanged(propertyName);
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual bool SetProperty<T>(
            ref ObservableCollection<T> storage,
            ObservableCollection<T> value,
            Action onChanged, string propertyName = null)
        {
            if (Equals(storage, value) || string.IsNullOrWhiteSpace(propertyName))
                return false;

            AddValueToOldValueDictionary(propertyName, storage);

            if (storage != null)
                storage.CollectionChanged -= StorageOnCollectionChanged;

            storage = value;

            if (storage != null)
                storage.CollectionChanged += StorageOnCollectionChanged;

            RaisePropertyChanged(propertyName);
            OnPropertyChanged(propertyName);
            return true;

            void StorageOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
                => AddValueToOldValueDictionary(propertyName, value.Clone());
        }

        private void AddValueToOldValueDictionary(string propertyName, object value)
        {
            if (OldValues.ContainsKey(propertyName))
                OldValues[propertyName].Add(value);
            else
                OldValues.Add(propertyName, new List<object> {value});
        }

        public virtual bool Undo(string propertyName)
        {
            if (OldValues == null)
                return false;

            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property cannot be null or whitespace", propertyName);

            if (!OldValues.ContainsKey(propertyName))
                return false;

            var property = GetType().GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException("Unknown property", propertyName);

            property.SetValue(this, OldValues[propertyName].Last());
            OldValues[propertyName].RemoveLast();

            if (OldValues[propertyName].Count == 0)
                OldValues.Remove(propertyName);

            RaisePropertyChanged(propertyName);
            return true;
        }

        public virtual void Save()
        {
            OldValues.Clear();
        }

        protected new virtual void OnPropertyChanged(string propertyName)
        {
        }

        #endregion METHODS
    }
}