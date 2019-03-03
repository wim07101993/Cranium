using System.Collections;
using System.Collections.Generic;

namespace Cranium.WPF.Game.GameBoard
{
    public class GameBoard : IReadOnlyList<Tile.Tile>
    {
        #region FIELDS

        private readonly List<Tile.Tile> _tiles;

        #endregion FIELDS


        #region CONSTRUCTORS

        public GameBoard(IEnumerable<Tile.Tile> tiles)
        {
            _tiles = new List<Tile.Tile>(tiles);
        }

        public GameBoard(int capacity)
        {
            _tiles = new List<Tile.Tile>(capacity);
        }
        
        public GameBoard()
        {
            _tiles = new List<Tile.Tile>();
        }

        #endregion CONSTRUCTORS


        #region PROPERTIES

        public Tile.Tile this[int index] => _tiles[index];

        public int Count => _tiles.Count;

        #endregion PROPERTIES


        #region METHODS

        #region ilist

        public bool Contains(Tile.Tile item) => _tiles.Contains(item);

        public void CopyTo(Tile.Tile[] array, int arrayIndex) => _tiles.CopyTo(array, arrayIndex);

        public IEnumerator<Tile.Tile> GetEnumerator() => _tiles.GetEnumerator();

        public int IndexOf(Tile.Tile item) => _tiles.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _tiles.GetEnumerator();
        #endregion ilist

        #endregion METHODS
    }
}
