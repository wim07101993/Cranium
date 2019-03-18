﻿using System;

namespace Cranium.Data.Exceptions
{
    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string identifier, string value)
            : base($"The {typeof(T).Name} with {identifier} {value} was not found.")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("This page doesn't exist.")
        {
        }

        protected NotFoundException(string message)
            : base(message)
        {
        }
    }
}
