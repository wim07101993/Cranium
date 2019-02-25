using System.Collections.Generic;
using Cranium.WPF.Models.Bases;
using MongoDB.Bson;

namespace Cranium.WPF.Models
{
    public class Tile : AWithId
    {
        public ObjectId CategoryId { get; set; }
        public bool IsShortCut { get; set; }

        public IList<Tile> NextTiles { get; set; } = new List<Tile>();

        public bool IsSplit => NextTiles?.Count > 1;
    }
}
