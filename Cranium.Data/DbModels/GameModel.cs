using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.DbModels
{
    public class GameModel : AModel
    {
        [BsonRequired, JsonRequired]
        [BsonElement("gameBoard"), JsonProperty("gameBoard")]
        public Guid GameBoard { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("players"), JsonProperty("players")]
        public IList<Guid> Players { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("questions"), JsonProperty("questions")]
        public IList<Guid> Questions { get; set; }

        [BsonElement("answeredQuestions"), JsonProperty("answeredQuestions")]
        public IList<Guid> AnsweredQuestions { get; set; }

        [BsonElement("currentPlayerIndex"), JsonProperty("currentPlayerIndex")]
        public int CurrentPlayerIndex { get; set; }
    }
}
