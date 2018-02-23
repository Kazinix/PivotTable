using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazinix.PivotTable
{
    public class PivotTableBuilder<TElement, TAggregates> : IPivotTableBuilder<TElement, TAggregates>
        where TElement : class
    {
        private readonly IList<Func<TElement, object>> _rowFunctions;
        private readonly IList<Func<TElement, object>> _columnFunctions;
        private readonly Func<IEnumerable<TElement>, TAggregates> _aggregateFunction;
        private readonly IEnumerable<TElement> _list;

        internal PivotTableBuilder(IEnumerable<TElement> list, Func<IEnumerable<TElement>, TAggregates> aggregateFunction)
        {
            _list = list;
            _aggregateFunction = aggregateFunction;
            _rowFunctions = new List<Func<TElement, object>>();
            _columnFunctions = new List<Func<TElement, object>>();
        }
        public IPivotTableBuilder<TElement, TAggregates> SetRow(Func<TElement, object> rowFunction)
        {
            _rowFunctions.Add(rowFunction);
            return this;
        }

        public IPivotTableBuilder<TElement, TAggregates> SetColumn(Func<TElement, object> columnFunction)
        {
            _columnFunctions.Add(columnFunction);
            return this;
        }

        public PivotTable<TAggregates> Build()
        {
            var pivotTable = new PivotTable<TAggregates>();

            //compute aggregates for the whole table
            pivotTable.Aggregates = _aggregateFunction.Invoke(_list);
            pivotTable.ColumnAggregates = ComputeColumns(_list, _columnFunctions);
            pivotTable.Rows = ComputeRows(_list, _rowFunctions, _columnFunctions);
            
            return pivotTable;
        }

        private IEnumerable<Row<TAggregates>> ComputeRows(IEnumerable<TElement> list, 
            IEnumerable<Func<TElement, object>> rowFunctions,
            IEnumerable<Func<TElement, object>> columnFunctions)
        {
            var rows = new List<Row<TAggregates>>();
            if(!rowFunctions.Any())
                return rows;

            //create a list that will be modified by the scope
            var rowFunctionsCopy = rowFunctions.ToList();

            //pop the row function
            var rowFunction = rowFunctionsCopy.First();
            rowFunctionsCopy.Remove(rowFunction);

            //group items by row
            var groups = list.GroupBy(rowFunction);
            
            foreach(var group in groups)
            {
                var newRow = new Row<TAggregates>();

                newRow.Value = group.Key;
                newRow.Aggregates = _aggregateFunction.Invoke(group.ToList());
                newRow.ColumnAggregates = ComputeColumns(group.ToList(), _columnFunctions);
                newRow.SubRows = ComputeRows(group.ToList(), rowFunctionsCopy, columnFunctions);

                rows.Add(newRow);
            }

            return rows;
        }

        private IEnumerable<Column<TAggregates>> ComputeColumns(IEnumerable<TElement> list, 
            IEnumerable<Func<TElement, object>> columnFunctions)
        {
            var columns = new List<Column<TAggregates>>();
            if(!columnFunctions.Any())
                return columns;

            //create a list that will be modified by the scope
            var columnFunctionsCopy = columnFunctions.ToList();

            //pop the column function 
            var columnFunction = columnFunctionsCopy.First();
            columnFunctionsCopy.Remove(columnFunction);

            //group items by column
            var groups = list.GroupBy(columnFunction);

            foreach(var group in groups)
            {
                var newColumn = new Column<TAggregates>();

                newColumn.Value = group.Key;
                newColumn.Aggregates = _aggregateFunction.Invoke(group.ToList());
                newColumn.SubColumns = ComputeColumns(group.ToList(), columnFunctionsCopy);

                columns.Add(newColumn);
            }
            
            return columns;
        }
    }
}