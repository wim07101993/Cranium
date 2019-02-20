using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cranium.Data.Models
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

        public IEnumerator<Tile> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        #endregion METHODS


        #region NESTED CLASSES

        public struct TileEnumerator : IEnumerator<Tile>
        {
            private IList<Tile> _tiles;
            private Tile _current;

            private List<Tile> _splits;



            internal TileEnumerator(IList<Tile> tiles)
            {
                _tiles = tiles;
                _current = null;
            }


            public Tile Current => _current;

            object IEnumerator.Current => Current;


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _tiles.Single(x => x.IsStart);
                    return true;
                }

                if (_current.IsEnd)
                {
                    _current = null;
                    return false;
                }


            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion NESTED CLASSES
    }
}
