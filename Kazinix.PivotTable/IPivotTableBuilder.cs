using System;
using System.Collections.Generic;
using System.Linq;

namespace Kazinix.PivotTable
{
    public interface IPivotTableBuilder<TElement, TAggregates> 
        where TElement : class
    {
        IPivotTableBuilder<TElement, TAggregates> SetRow(Func<TElement, object> rowFunction);
        IPivotTableBuilder<TElement, TAggregates> SetColumn(Func<TElement, object> columnFunction);
        PivotTable<TAggregates> Build();
    }
}
