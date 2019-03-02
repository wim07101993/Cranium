﻿using Cranium.WPF.Models.Bases;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Cranium.WPF.Services.Game
{
    public class Tile : AWithId
    {
        public Tile(ObjectId id, ObjectId categoryId)
        {
            Id = id;
            CategoryId = categoryId;
        }


        public ObjectId CategoryId { get; }

        public IList<Player> Players { get; } = new List<Player>();
    }
}
