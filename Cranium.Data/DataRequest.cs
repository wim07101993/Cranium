using System;
using System.Linq.Expressions;

namespace Cranium.Data
{
    public class DataRequest<T>
    {
        public Expression<Func<T, bool>> Where { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
    }
}
