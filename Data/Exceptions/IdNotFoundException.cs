using System;

namespace Data.Exceptions
{
    public class IdNotFoundException : DataException
    {
        public IdNotFoundException() { }

        public IdNotFoundException(long id, Type type, string message = null)
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


        public long Id { get; }
        public Type Type { get; }

        public override string Message { get; }
    }
}
