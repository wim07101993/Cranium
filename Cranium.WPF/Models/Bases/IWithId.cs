using System.ComponentModel;
using MongoDB.Bson;

namespace Cranium.WPF.Models.Bases
{
    public interface IWithId : INotifyPropertyChanged
    {
        ObjectId Id { get; set; }
    }
}
