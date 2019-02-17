using System;
using System.ComponentModel;

namespace Cranium.Data.ViewModels.Models.Bases
{
    public interface IWithId : INotifyPropertyChanged
    {
        Guid Id { get; set; }
    }
}
