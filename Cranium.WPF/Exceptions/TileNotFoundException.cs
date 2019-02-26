using System;

namespace Cranium.WPF.Exceptions
{
    public class TileNotFoundException : GameException
    {
        public TileNotFoundException()
        {
        }

        public TileNotFoundException(string message) : base(message)
        {
        }

        public TileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
