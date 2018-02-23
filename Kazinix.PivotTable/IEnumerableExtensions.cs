using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Kazinix.PivotTable
{
    public static class IEnumerableExtensions
    {
        public static IPivotTableBuilder<TElement, TAggregates> GetPivotTableBuilder<TElement, TAggregates>(this IEnumerable<TElement> list,
            Func<IEnumerable<TElement>, TAggregates> aggregates)
            where TElement : class
        {
            return new PivotTableBuilder<TElement, TAggregates>(list, aggregates);
        }
    }
}

