using System.Collections.Generic;
using Cranium.WPF.Models;

namespace Cranium.WPF.ViewModels.Game
{
    public interface IControlWindowViewModel : ICollectionViewModel<IPlayerViewModel>
    {
        List<Category> Categories { get; }
    }
}
