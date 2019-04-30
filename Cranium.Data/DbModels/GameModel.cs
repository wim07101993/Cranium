using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cranium.Data.DbModels
{
    public class GameModel : AWithId
    {
        [BsonRequired, JsonRequired]
        [BsonElement("gameBoard"), JsonProperty("gameBoard")]
        public Guid GameBoard { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("teams"), JsonProperty("teams")]
        public IList<Guid> Teams { get; set; }

        [BsonRequired, JsonRequired]
        [BsonElement("tasks"), JsonProperty("tasks")]
        public IList<Guid> Tasks { get; set; }

        [BsonElement("completedTasks"), JsonProperty("completedTasks")]
        public IList<Guid> CompletedTasks { get; set; }

        [BsonElement("currentTeamIndex"), JsonProperty("currentTeamIndex")]
        public int CurrentTeamIndex { get; set; }
    }
}
