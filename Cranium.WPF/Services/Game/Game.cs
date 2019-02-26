using Cranium.WPF.Models;
using Cranium.WPF.Models.Bases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cranium.WPF.Services.Game
{
    public class Game : AWithId
    {
        #region FIELDS

        #endregion FIELDS


        #region CONSTRUCTOR

        public Game(GameBoard gameBoard, IEnumerable<Player> players, IEnumerable<Question> questions)
        {
            Questions = questions;
            GameBoard = gameBoard;
            Players = new ReadOnlyCollection<Player>(players.ToList());

        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public GameBoard GameBoard { get; }

        public IReadOnlyCollection<Player> Players { get; }

        public IEnumerable<Question> Questions { get; }

        #endregion PROPERTIES
    }
}
