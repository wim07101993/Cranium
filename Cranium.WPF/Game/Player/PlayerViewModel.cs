using System.Threading.Tasks;
using System.Windows.Input;
using Cranium.WPF.Data.Category;
using Cranium.WPF.Helpers.ViewModels;
using Cranium.WPF.Strings;
using Prism.Commands;

namespace Cranium.WPF.Game.Player
{
    public class PlayerViewModel : AModelContainerViewModel<Player>
    {
        #region FIELDS

        private readonly IGameService _gameService;

        private bool _moveBackward;

        #endregion FIELDS


        #region CONSTRUCTOR

        public PlayerViewModel(
            IStringsProvider stringsProvider, IGameService gameService)
            : base(stringsProvider)
        {
            _gameService = gameService;

            SelectCategoryCommand = new DelegateCommand<Category>(x => { var _ = MovePlayerAsync(x); });
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES
        
        public ICommand SelectCategoryCommand { get; }

        public Category Category
        {
            get => null;
            set
            {
                var _ = MovePlayerAsync(value);
                RaisePropertyChanged();
            }
        }

        public bool MoveBackward
        {
            get => _moveBackward;
            set => SetProperty(ref _moveBackward, value);
        }

        #endregion PROPERTIES


        #region METHODS
        
        private async Task MovePlayerAsync(Category category)
        {
            if (MoveBackward)
                await _gameService.MovePlayerBackwardsToAsync(Model.Id, category.Id);
            else
                await _gameService.MovePlayerToAsync(Model.Id, category.Id);
        }

        #endregion METHODS
    }
}