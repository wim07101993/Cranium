using System.ComponentModel;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.ViewModels
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        Strings Strings { get; }
    }
}
