using Cranium.Data.Models.Bases;
using System;
using System.Collections.Generic;

namespace Cranium.Data.Models
{
    public class Tile : AWithId
    {
        public Guid CategoryId { get; set; }
        public bool IsShortCut { get; set; }

        public ICollection<Guid> NextTiles { get; set; }

        public bool IsEnd { get; set; }
        public bool IsStart { get; set; }
    }
}
