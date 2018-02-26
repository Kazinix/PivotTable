## Description

A [NuGet library](https://www.nuget.org/packages/Kazinix.PivotTable) that you can add into your project and will extend your `IEnumerable` of objects. 

## Usage

It's like using any pivot table in spreadsheets. You can set properties as rows, columns and as values to be computed.

```csharp
var expenses = new List<Expenses>()
	{
		new Expenses { Category = "Food", Date = new DateTime(2018, 01, 1), Amount = 100 },
		new Expenses { Category = "Food", Date = new DateTime(2018, 01, 2), Amount = 12 },
		new Expenses { Category = "Travel", Date = new DateTime(2018, 01, 3), Amount = 55 },
		new Expenses { Category = "Travel", Date = new DateTime(2018, 03, 4), Amount = 101 },
		new Expenses { Category = "Toiletries", Date = new DateTime(2018, 03, 6), Amount = 67 },
		new Expenses { Category = "Car", Date = new DateTime(2018, 04, 22), Amount = 796 },
		new Expenses { Category = "Food", Date = new DateTime(2018, 05, 13), Amount = 9 },
		new Expenses { Category = "Toiletries", Date = new DateTime(2018, 05, 14), Amount = 32 },
		new Expenses { Category = "Food", Date = new DateTime(2018, 06, 3), Amount = 345 },
		new Expenses { Category = "Car", Date = new DateTime(2018, 06, 7), Amount = 8 }
	};

var pivotTable = expenses
	.GetPivotTableBuilder(l => l.Sum(e => e.Amount))
	.SetRow(e => e.Category)
	.SetColumn(e => e.Date)
	.Build();
```
## Compute Multiple Fields

```csharp
var pivotTable = expenses
	.GetPivotTableBuilder(l => new {
		SumOfAmount = l.Sum(e => e.Amount),
		AverageOfAmount = l.Average(e => e.Amount)
	})
	.SetRow(e => e.Category)
	.SetColumn(e => e.Date)
	.Build();
```

