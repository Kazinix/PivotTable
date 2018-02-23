using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kazinix.PivotTable;
using Newtonsoft.Json;

namespace PivotTableSamples
{
    class Program
    {

        static void Main(string[] args)
        {
            Test();
        }

        static void Test()
        {
            var expenses = new List<Expense>()
            {
                new Expense { Category = "Food", Date = new DateTime(2018, 01, 1), Amount = 100 },
                new Expense { Category = "Food", Date = new DateTime(2018, 01, 2), Amount = 12 },
                new Expense { Category = "Travel", Date = new DateTime(2018, 01, 3), Amount = 55 },
                new Expense { Category = "Travel", Date = new DateTime(2018, 03, 4), Amount = 101 },
                new Expense { Category = "Toiletries", Date = new DateTime(2018, 03, 6), Amount = 67 },
                new Expense { Category = "Car", Date = new DateTime(2018, 04, 22), Amount = 796 },
                new Expense { Category = "Food", Date = new DateTime(2018, 05, 13), Amount = 9 },
                new Expense { Category = "Toiletries", Date = new DateTime(2018, 05, 14), Amount = 32 },
                new Expense { Category = "Food", Date = new DateTime(2018, 06, 3), Amount = 345 },
                new Expense { Category = "Car", Date = new DateTime(2018, 06, 7), Amount = 8 }
            };

            var timeStarted = DateTime.Now;

            var pivotTable = expenses
                .GetPivotTableBuilder(l => l.Sum(e => e.Amount))
                .SetRow(e => e.Category)
                .SetColumn(e => e.Date)
                .Build();

            var timeFinished = DateTime.Now;

            Console.WriteLine($"Started: {timeStarted}, Finsihed: {timeFinished}, Duration: {timeFinished - timeStarted}");
            Console.WriteLine(JsonConvert.SerializeObject(pivotTable)); 
        }
    }
}
