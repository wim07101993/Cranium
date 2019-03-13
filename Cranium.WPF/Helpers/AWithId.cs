using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Cranium.WPF.Helpers
{
    public abstract class AWithId : BindableBase, IWithId
    {
        #region FIELDS

        private ObjectId _id;

        #endregion FIELDS


        #region CONSTRUCTOR

        public AWithId()
        {
            PropertyChanged += OnPropertyChanged;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        [BsonId]
        public ObjectId Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonIgnore]
        [BsonIgnore]
        public ObservableCollection<string> ChangedProperties { get; } = new ObservableCollection<string>();

        #endregion PROPERTIES


        #region METHODS

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ChangedProperties.Any(x => x == e.PropertyName))
                return;

            ChangedProperties.Add(e.PropertyName);

            var property = GetType().GetProperty(e.PropertyName);
            if (typeof(INotifyCollectionChanged).IsAssignableFrom(property.PropertyType))
            {
                var collectionChanged = (INotifyCollectionChanged)property.GetValue(this);
                collectionChanged.CollectionChanged += (s, carg) => OnPropertyChanged(this, e);
            }
        }

        #endregion METHODS
    }
}