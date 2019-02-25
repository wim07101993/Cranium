using Cranium.WPF.Models.Bases;
using System.Collections;
using System.Collections.Generic;

namespace Cranium.WPF.Services.Game
{
    public class Player : AWithId, IEnumerator<Tile>
    {
        private readonly IGameService _gameService;


        public Player(IGameService gameService)
        {
            _gameService = gameService;
        }


        public Tile Current { get; }

        object IEnumerator.Current => Current;


        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
            
        }
    }
}
