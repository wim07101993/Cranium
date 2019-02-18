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

        /// <summary>
        /// Dictionary to store all old values of properties
        /// </summary>
        private readonly Dictionary<string, IList<object>>
            _oldValueDictionary = new Dictionary<string, IList<object>>();

        #endregion FIELDS


        #region PROPERTIES

        [JsonIgnore]
        public virtual IReadOnlyCollection<string> ChangedProperties
            => new ReadOnlyCollection<string>(_oldValueDictionary.Keys.ToList());

        [JsonIgnore]
        public virtual IReadOnlyDictionary<string, IList<object>> OldValues
            => _oldValueDictionary;

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
            if (_oldValueDictionary.ContainsKey(propertyName))
                _oldValueDictionary[propertyName].Add(value);
            else
                _oldValueDictionary.Add(propertyName, new List<object> {value});
        }

        public virtual bool Undo(string propertyName)
        {
            if (_oldValueDictionary == null)
                return false;

            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property cannot be null or whitespace", propertyName);

            if (!_oldValueDictionary.ContainsKey(propertyName))
                return false;

            var property = GetType().GetProperty(propertyName);

            if (property == null)
                throw new ArgumentException("Unknown property", propertyName);

            property.SetValue(this, _oldValueDictionary[propertyName].Last());
            _oldValueDictionary[propertyName].RemoveLast();

            if (_oldValueDictionary[propertyName].Count == 0)
                _oldValueDictionary.Remove(propertyName);

            RaisePropertyChanged(propertyName);
            return true;
        }

        protected new virtual void OnPropertyChanged(string propertyName)
        {
        }

        #endregion METHODS
    }
}