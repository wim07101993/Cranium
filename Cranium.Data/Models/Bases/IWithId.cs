using System;

namespace Cranium.Data.Models.Bases
{
    public interface IWithId
    {
        Guid Id { get; set; }
    }
}
