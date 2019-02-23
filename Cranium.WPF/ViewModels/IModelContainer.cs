using Cranium.WPF.Models.Bases;

namespace Cranium.WPF.ViewModels
{
    public interface IModelContainer<T> : IViewModelBase where T : IWithId
    {
        T Model { get; set; }
    }
}
