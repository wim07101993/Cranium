using System;

namespace Cranium.Data.Exceptions
{
    public class IdNotFoundException : DataException
    {
        public IdNotFoundException() { }

        public IdNotFoundException(Guid id, Type type, string message = null)
        {
            Id = id;
            Type = type;
            Message = message ?? $"There was no {type.Name} found with id {id}.";
        }

        public IdNotFoundException(string message)
        {
            Message = message;
        }

        public IdNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            Message = message;
        }


        public Guid Id { get; }
        public Type Type { get; }

        public override string Message { get; }
    }
}
