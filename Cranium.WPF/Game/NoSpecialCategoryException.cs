﻿using System;

namespace Cranium.WPF.Game
{
    public class NoSpecialCategoryException : GameException
    {
        public NoSpecialCategoryException()
        {
        }

        public NoSpecialCategoryException(string message) : base(message)
        {
        }

        public NoSpecialCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
