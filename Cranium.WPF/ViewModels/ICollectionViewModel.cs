using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cranium.WPF.ViewModels
{
    public interface ICollectionViewModel<T> : IViewModelBase
    {
        ObservableCollection<T> ItemsSource { get; }

        ICommand CreateCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand UpdateCollectionCommand { get; }

        Task UpdateTask { get; }


        Task CreateAsync();
        Task DeleteAsync(T viewModel);
        Task UpdateCollectionAsync();
    }
}
