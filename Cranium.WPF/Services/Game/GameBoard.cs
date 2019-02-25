using System.Collections;
using System.Collections.Generic;

namespace Cranium.WPF.Services.Game
{
    public class GameBoard : IReadOnlyList<Tile>
    {
        #region FIELDS

        private readonly List<Tile> _tiles;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameBoard(IEnumerable<Tile> tiles)
        {
            _tiles = new List<Tile>(tiles);
        }

        public GameBoard(int capacity)
        {
            _tiles = new List<Tile>(capacity);
        }
        
        public GameBoard()
        {
            _tiles = new List<Tile>();
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Tile this[int index] => _tiles[index];

        public int Count => _tiles.Count;

        #endregion PROPERTIES


        #region METHODS

        #region ilist

        public bool Contains(Tile item) => _tiles.Contains(item);

        public void CopyTo(Tile[] array, int arrayIndex) => _tiles.CopyTo(array, arrayIndex);

        public IEnumerator<Tile> GetEnumerator() => _tiles.GetEnumerator();

        public int IndexOf(Tile item) => _tiles.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _tiles.GetEnumerator();
        #endregion ilist

        #endregion METHODS
    }
}
