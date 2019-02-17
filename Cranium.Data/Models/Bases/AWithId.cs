using System;

namespace Cranium.Data.Models.Bases
{
    public abstract class AWithId : IWithId
    {
        public Guid Id { get; set; }
    }
}
