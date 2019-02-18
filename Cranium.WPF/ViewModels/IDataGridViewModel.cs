using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cranium.WPF.ViewModels
{
    public interface IDataGridViewModel<T> : IViewModelBase
    {
        ObservableCollection<T> ItemsSource { get; }

        ICommand CreateCommand { get; }
        
        ICommand SaveCommand { get; }

        ICommand DeleteCommand { get; }


        void Create();
        Task SaveAsync();
        void Delete(T item);
    }
}
