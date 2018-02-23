using System;
using System.Collections.Generic;

namespace Kazinix.PivotTable
{
    public class PivotTable<TAggregates>
    {
        internal PivotTable(){}
        public TAggregates Aggregates { get; set; }
        public IEnumerable<Column<TAggregates>> ColumnAggregates { get; set; }
        public IEnumerable<Row<TAggregates>> Rows { get; set; }
    }
}