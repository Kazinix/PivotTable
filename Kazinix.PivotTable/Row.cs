using System;
using System.Collections.Generic;

namespace Kazinix.PivotTable
{
    public class Row<TAggregates>
    {
        internal Row(){}
        public object Value { get; set; }
        public TAggregates Aggregates { get; set; }
        public IEnumerable<Column<TAggregates>> ColumnAggregates { get; set; }
        public IEnumerable<Row<TAggregates>> SubRows { get; set; }
    }
}