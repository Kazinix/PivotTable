using System;
using System.Collections.Generic;

namespace Kazinix.PivotTable
{
    public class Column<TAggregates>
    {
        internal Column(){}
        public object Value { get; set; }
        public TAggregates Aggregates { get; set; }
        public IEnumerable<Column<TAggregates>> SubColumns { get; set; }
    }
}