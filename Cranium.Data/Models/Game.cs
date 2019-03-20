using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Cranium.Data.Models
{
    public class Game : BindableBase
    {
        #region FIELDS

        private GameBoard _gameBoard;
        private ObservableCollection<Team> _teams;
        private ObservableCollection<Task> _tasks;
        private ObservableCollection<Task> _completedTasks;
        private int _currentTeamIndex;

        #endregion FIELDS


        #region PROPERTIES

        public GameBoard GameBoard
        {
            get => _gameBoard;
            set => SetProperty(ref _gameBoard, value);
        }

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            internal set => SetProperty(ref _teams, value);
        }

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            internal set => SetProperty(ref _tasks, value);
        }

        public ObservableCollection<Task> CompletedTasks
        {
            get => _completedTasks;
            internal set => SetProperty(ref _completedTasks, value);
        }

        public int CurrentTeamIndex
        {
            get => _currentTeamIndex;
            set => SetProperty(ref _currentTeamIndex, value);
        }

        #endregion PROPERTIES
    }
}
