using System;
using System.ComponentModel;

namespace Cranium.Data.RestClient.Models.Bases
{
    public interface IWithId : INotifyPropertyChanged
    {
        Guid Id { get; set; }
    }
}
