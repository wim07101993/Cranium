using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cranium.WPF.Models
{
    public class Tiles : ICollection<Tile>
    {
        #region FIELDS

        private List<Tile> _tiles;

        #endregion FIELDS


        #region CONSTRUCTORS

        public Tiles()
        {
            _tiles = new List<Tile>();
        }

        public Tiles(int capacity)
        {
            _tiles = new List<Tile>(capacity);
        }

        public Tiles(IEnumerable<Tile> tiles)
        {
            _tiles = new List<Tile>(tiles);
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public int Count => _tiles.Count;

        public bool IsReadOnly => false;

        #endregion PROPERTIES


        #region METHODS

        public void Add(Tile tile) => _tiles.Add(tile);

        public void Clear() => _tiles.Clear();

        public bool Contains(Tile tile) => _tiles.Contains(tile);

        public void CopyTo(Tile[] array, int arrayIndex) => _tiles.CopyTo(array, arrayIndex);

        public bool Remove(Tile tile) => _tiles.Remove(tile);

        public IEnumerator<Tile> GetEnumerator() => _tiles.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public Tile GetNext(Tile tile)
        {
            if (tile.IsEnd)
                return null;

            return this.Single(x => x.Id == tile.NextTiles[0]);
        }

        public IEnumerable<Tile> GetNextUntilSplitOrConvergence(Tile tile)
        {   
            while (true)
            {
                // if it is the last, there are no next
                if (tile.IsEnd)
                    yield break;

                tile = GetNext(tile);

                if (tile.IsSplit || IsConvergent(tile))
                    yield break;

                yield return tile;
            }
        }

        public Tile GetPrevious(Tile tile)
        {
            if (tile.IsStart)
                return null;

            return this.Single(t => t.NextTiles.Any(id => id == tile.Id));
        }

        public IEnumerable<Tile> GetPreviousUntilSplitOrConvergence(Tile tile)
        {
            while (true)
            {
                // if it is the start, there are no previous
                if (tile.IsStart)
                    yield break;

                tile = GetPrevious(tile);

                if (tile.IsSplit || IsConvergent(tile))
                    yield break;

                yield return tile;
            }
        }

        private bool IsConvergent(Tile tile) => this.Any(t => !t.IsEnd && t.NextTiles.Count(id => id == tile.Id) > 1);

        #endregion METHODS
    }
}
