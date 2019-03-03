using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Cranium.WPF.Helpers.Mongo
{
    public static class MongoExtensions
    {
        public static IFindFluent<T, T> Select<T>(this IFindFluent<T, T> This,
            IEnumerable<Expression<Func<T, object>>> propertiesToInclude)
            where T : IWithId
        {
            if (propertiesToInclude == null)
                return This;

            var selector = Builders<T>.Projection.Include(x => x.Id);

            selector = propertiesToInclude.Aggregate(selector, (current, property) => current.Include(property));

            return This.Project<T>(selector);
        }
    }
}
