using System.Collections.ObjectModel;

namespace Shared
{
    public interface IRecordChangedProperties
    {
        ObservableCollection<string> ChangedProperties { get; }
    }
}
