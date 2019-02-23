using Cranium.Data.Models.Bases;
using System;
using System.Collections.Generic;

namespace Cranium.Data.Models
{
    public class Tile : AWithId
    {
        public Guid CategoryId { get; set; }
        public bool IsShortCut { get; set; }

        public IList<Guid> NextTiles { get; set; }

        public bool IsEnd => NextTiles == null || NextTiles.Count == 0;

        public bool IsStart { get; set; }

        public bool IsSplit => NextTiles?.Count > 1;
    }
}
