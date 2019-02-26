﻿using System;

namespace Cranium.WPF.Exceptions
{
    public class PlayerNotFoundException : GameException
    {
        public PlayerNotFoundException()
        {
        }

        public PlayerNotFoundException(string message) : base(message)
        {
        }

        public PlayerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
