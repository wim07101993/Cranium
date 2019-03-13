using System.Collections.Generic;
using Cranium.WPF.Game.GameBoard;
using Cranium.WPF.Game.Player;
using Cranium.WPF.Helpers;
using MongoDB.Bson.Serialization.Attributes;

namespace Cranium.WPF.Data.Game
{
    public class Game : AWithId
    {
        [BsonElement("gameBoard")]
        public GameBoard GameBoard { get; set; }

        [BsonElement("players")]
        public IList<Player> Players { get; set; }

        [BsonElement("questions")]
        public IList<Data.Question.Question> Questions { get; set; }

        [BsonElement("answeredQuestions")]
        public IList<Data.Question.Question> AnsweredQuestions { get; set; }

        [BsonElement("currentPlayerIndex")]
        public int CurrentPlayerIndex { get; set; }
    }
}