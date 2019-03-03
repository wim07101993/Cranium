using Cranium.WPF.Game.Player;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Unity;

namespace Cranium.WPF.Game.Control
{
    public class ControlWindowViewModel : AViewModelBase
    {
        #region FIELDS
        
        #endregion FIELDS


        #region CONSTRUCTOR

        public ControlWindowViewModel(IUnityContainer unityContainer)
            : base(unityContainer.Resolve<IStringsProvider>())
        {
            PlayersViewModel = unityContainer.Resolve<PlayersViewModel>();
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public PlayersViewModel PlayersViewModel { get; }

        #endregion PROPERTIES


        #region METHODS

        #endregion METHODS
    }
}