using System;

namespace Cranium.WPF.Game
{
    public class NoCategoriesException : GameException
    {
        public NoCategoriesException()
        {
        }

        public NoCategoriesException(string message) : base(message)
        {
        }

        public NoCategoriesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
