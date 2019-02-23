using System;
using System.Collections.Generic;
using Cranium.WPF.Models.Bases;
using MongoDB.Bson;

namespace Cranium.WPF.Models
{
    public class Tile : AWithId
    {
        public Guid CategoryId { get; set; }
        public bool IsShortCut { get; set; }

        public IList<ObjectId> NextTiles { get; set; }

        public bool IsEnd => NextTiles == null || NextTiles.Count == 0;

        public bool IsStart { get; set; }

        public bool IsSplit => NextTiles?.Count > 1;
    }
}
