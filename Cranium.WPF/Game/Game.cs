using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cranium.WPF.Helpers;
using Cranium.WPF.Helpers.Extensions;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Game
{
    public class Game : AWithId
    {
        #region FIELDS

        private int _currentPlayerIndex;

        #endregion FIELDS


        #region CONSTRUCTOR

        public Game(GameBoard.GameBoard gameBoard, IEnumerable<Player.Player> players, IList<Data.Question.Question> questions)
        {
            Questions = questions;
            GameBoard = gameBoard;
            Players = new ObservableCollection<Player.Player>(players);
            GameBoard[0].Players.Add(Players);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        [BsonElement("gameBoard")]
        public GameBoard.GameBoard GameBoard { get; }

        [BsonElement("players")]
        public ObservableCollection<Player.Player> Players { get; }

        [BsonElement("questions")]
        public IList<Data.Question.Question> Questions { get; }

        [BsonElement("currentPlayerIndex")]
        public int CurrentPlayerIndex
        {
            get => _currentPlayerIndex;
            set
            {
                if (!SetProperty(ref _currentPlayerIndex, value))
                    return;
                RaisePropertyChanged(nameof(CurrentPlayer));
            }
        }

        [BsonIgnore]
        public Player.Player CurrentPlayer => Players[CurrentPlayerIndex];

        [BsonIgnore]
        public Tile.Tile TileOfCurrentPlayer
            => GameBoard.First(tile => tile.Players.Any(player => player.Id == CurrentPlayer.Id));

        #endregion PROPERTIES
    }
}