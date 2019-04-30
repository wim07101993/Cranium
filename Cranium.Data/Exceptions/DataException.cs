using System;

namespace Cranium.Data.Exceptions
{
    public class DataException : AggregateException
    {
        public DataException()
        {
        }

        public DataException(ECrudMethod databaseMethod, params Exception[] innerExceptions)
            : base($"Something went wrong while trying to {databaseMethod.ToString()} an element to/from the database.", innerExceptions)
        {
            DatabaseMethod = databaseMethod;
        }


        public ECrudMethod DatabaseMethod { get; set; }
    }
}
