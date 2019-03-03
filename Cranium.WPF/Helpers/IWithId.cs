using System.ComponentModel;
using MongoDB.Bson;

namespace Cranium.WPF.Helpers
{
    public interface IWithId : INotifyPropertyChanged
    {
        ObjectId Id { get; set; }
    }
}
