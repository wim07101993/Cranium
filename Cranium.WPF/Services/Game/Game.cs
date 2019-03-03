using Cranium.WPF.Models;
using Cranium.WPF.Models.Bases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cranium.WPF.Extensions;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Services.Game
{
    public class Game : AWithId
    {
        #region FIELDS

        private int _currentPlayerIndex;

        #endregion FIELDS


        #region CONSTRUCTOR

        public Game(GameBoard gameBoard, IEnumerable<Player> players, IList<Question> questions)
        {
            Questions = questions;
            GameBoard = gameBoard;
            Players = new ObservableCollection<Player>(players);
            GameBoard[0].Players.Add(Players);
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        [BsonElement("gameBoard")]
        public GameBoard GameBoard { get; }

        [BsonElement("players")]
        public ObservableCollection<Player> Players { get; }

        [BsonElement("questions")]
        public IList<Question> Questions { get; }

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
        public Player CurrentPlayer => Players[CurrentPlayerIndex];

        [BsonIgnore]
        public Tile TileOfCurrentPlayer
            => GameBoard.First(tile => tile.Players.Any(player => player.Id == CurrentPlayer.Id));

        #endregion PROPERTIES
    }
}