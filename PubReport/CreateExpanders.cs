using Microsoft.Maui.Controls.Shapes;

using Syncfusion.Maui.Expander;

namespace PubReport;

internal class CreateExpanders
{
	private static DateTime _fromDateTime;
	private static DateTime _toDateTime;

	internal static async Task LoadExpandersData(DateTime fromDateTime, DateTime toDateTime, Grid expanderGrid)
	{
		_fromDateTime = fromDateTime;
		_toDateTime = toDateTime;

		expanderGrid.Children.Clear();
		expanderGrid.RowDefinitions.Clear();

		List<TransactionTotalsModel> transactionTotalsModels = [];

		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location))
		{
			var transactionTotal = await TransactionData.LoadTransactionTotalsByDateLocation(_fromDateTime, _toDateTime, location.Id);
			transactionTotal ??= new TransactionTotalsModel { LocationId = location.Id };
			transactionTotalsModels.Add(transactionTotal);

			expanderGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			var expander = CreateLocationExpander(transactionTotal, location.Name, location.Id);
			Grid.SetRow(expander, expanderGrid.RowDefinitions.Count - 1);
			expanderGrid.Children.Add(expander);
		}

		TransactionTotalsModel granTransactionTotal = new()
		{
			LocationId = 0,
			Male = transactionTotalsModels.Sum(x => x.Male),
			Female = transactionTotalsModels.Sum(x => x.Female),
			Loyalty = transactionTotalsModels.Sum(x => x.Loyalty),
			Cash = transactionTotalsModels.Sum(x => x.Cash),
			Card = transactionTotalsModels.Sum(x => x.Card),
			UPI = transactionTotalsModels.Sum(x => x.UPI),
			Amex = transactionTotalsModels.Sum(x => x.Amex),
		};

		expanderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
		expanderGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		var totalExpander = CreateLocationExpander(granTransactionTotal, "Total");
		Grid.SetRow(totalExpander, expanderGrid.RowDefinitions.Count - 1);
		expanderGrid.Children.Add(totalExpander);
	}

	private static Border CreateLocationExpander(TransactionTotalsModel transactionTotal, string locationName, int locationId = 0)
	{
		var border = new Border
		{
			Margin = 10,
			StrokeShape = new RoundRectangle
			{
				CornerRadius = new CornerRadius(8)
			},
			Stroke = new SolidColorBrush(Color.FromArgb("#CAC4D0")),
			StrokeThickness = 1
		};

		// Expander
		var expander = new SfExpander
		{
			Margin = new Thickness(10),
			HeaderBackground = new SolidColorBrush(Color.FromArgb("#ddb7fd"))
		};

		#region Header

		// Expander Header
		var headerGrid = new Grid();
		expander.Header = headerGrid;

		// Header Grid Column Definitions
		headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

		// Header controls
		var locationText = new Label
		{
			TextColor = Colors.Black,
			Text = locationName == "Total" ? "Grand Total" : $"{locationName}",
			Margin = new Thickness(10)
		};
		Grid.SetColumn(locationText, 0);
		headerGrid.Children.Add(locationText);

		var amountLabel = new Label
		{
			Text = (transactionTotal.Cash + transactionTotal.Card + transactionTotal.UPI + transactionTotal.Amex).ToString(),
			Margin = new Thickness(10)
		};

		Grid.SetColumn(amountLabel, 2);
		headerGrid.Children.Add(amountLabel);

		#endregion

		#region Body

		// Expander Content
		var contentGrid = new Grid()
		{
			Margin = 10,
		};
		expander.Content = contentGrid;

		// Content Grid Column Definitions
		contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
		contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

		// People Grid
		var peopleGrid = CreatePeopleGrid(transactionTotal);
		Grid.SetColumn(peopleGrid, 0);
		contentGrid.Children.Add(peopleGrid);

		// Amount Grid
		var amountGrid = CreateAmountGrid(transactionTotal);
		Grid.SetColumn(amountGrid, 2);
		contentGrid.Children.Add(amountGrid);

		#endregion

		border.Content = expander;

		return border;
	}

	private static Grid CreatePeopleGrid(TransactionTotalsModel transactionTotal)
	{
		var grid = new Grid();
		// Column definitions
		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

		// Row definitions
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

		// Controls
		var totalPeopleText = new Label { Text = "People", Margin = new Thickness(10) };
		Grid.SetRow(totalPeopleText, 0);
		Grid.SetColumn(totalPeopleText, 0);
		grid.Children.Add(totalPeopleText);

		var peopleTextBox = new Entry
		{
			Text = (transactionTotal.Male + transactionTotal.Female).ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(peopleTextBox, 0);
		Grid.SetColumn(peopleTextBox, 1);
		grid.Children.Add(peopleTextBox);


		var maleText = new Label { Text = "Male", Margin = new Thickness(10) };
		Grid.SetRow(maleText, 2);
		Grid.SetColumn(maleText, 0);
		grid.Children.Add(maleText);

		var maleTextBox = new Entry
		{
			Text = transactionTotal.Male.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(maleTextBox, 2);
		Grid.SetColumn(maleTextBox, 1);
		grid.Children.Add(maleTextBox);


		var femaleText = new Label { Text = "Female", Margin = new Thickness(10) };
		Grid.SetRow(femaleText, 3);
		Grid.SetColumn(femaleText, 0);
		grid.Children.Add(femaleText);

		var femaleTextBox = new Entry
		{
			Text = transactionTotal.Female.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(femaleTextBox, 3);
		Grid.SetColumn(femaleTextBox, 1);
		grid.Children.Add(femaleTextBox);


		var loyaltyText = new Label { Text = "Loyalty", Margin = new Thickness(10) };
		Grid.SetRow(loyaltyText, 4);
		Grid.SetColumn(loyaltyText, 0);
		grid.Children.Add(loyaltyText);

		var loyaltyTextBox = new Entry
		{
			Text = transactionTotal.Loyalty.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(loyaltyTextBox, 4);
		Grid.SetColumn(loyaltyTextBox, 1);
		grid.Children.Add(loyaltyTextBox);

		return grid;
	}

	private static Grid CreateAmountGrid(TransactionTotalsModel transactionTotal)
	{
		var grid = new Grid();
		// Column definitions
		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
		grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

		// Row definitions
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
		grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

		// Controls
		var amountText = new Label { Text = "Amount", Margin = new Thickness(10) };
		Grid.SetRow(amountText, 0);
		Grid.SetColumn(amountText, 0);
		grid.Children.Add(amountText);

		var amountTextBox = new Entry
		{
			Text = (transactionTotal.Cash + transactionTotal.Card + transactionTotal.UPI + transactionTotal.Amex).ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(amountTextBox, 0);
		Grid.SetColumn(amountTextBox, 1);
		grid.Children.Add(amountTextBox);


		var cashText = new Label { Text = "Cash", Margin = new Thickness(10) };
		Grid.SetRow(cashText, 2);
		Grid.SetColumn(cashText, 0);
		grid.Children.Add(cashText);

		var cashTextBox = new Entry
		{
			Text = transactionTotal.Cash.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(cashTextBox, 2);
		Grid.SetColumn(cashTextBox, 1);
		grid.Children.Add(cashTextBox);


		var cardText = new Label { Text = "Card", Margin = new Thickness(10) };
		Grid.SetRow(cardText, 3);
		Grid.SetColumn(cardText, 0);
		grid.Children.Add(cardText);

		var cardTextBox = new Entry
		{
			Text = transactionTotal.Card.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(cardTextBox, 3);
		Grid.SetColumn(cardTextBox, 1);
		grid.Children.Add(cardTextBox);


		var upiText = new Label { Text = "UPI", Margin = new Thickness(10) };
		Grid.SetRow(upiText, 4);
		Grid.SetColumn(upiText, 0);
		grid.Children.Add(upiText);

		var upiTextBox = new Entry
		{
			Text = transactionTotal.UPI.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(upiTextBox, 4);
		Grid.SetColumn(upiTextBox, 1);
		grid.Children.Add(upiTextBox);


		var amexText = new Label { Text = "Amex", Margin = new Thickness(10) };
		Grid.SetRow(amexText, 5);
		Grid.SetColumn(amexText, 0);
		grid.Children.Add(amexText);

		var amexTextBox = new Entry
		{
			Text = transactionTotal.Amex.ToString(),
			HorizontalTextAlignment = TextAlignment.End,
			IsReadOnly = true
		};
		Grid.SetRow(amexTextBox, 5);
		Grid.SetColumn(amexTextBox, 1);
		grid.Children.Add(amexTextBox);

		return grid;
	}
}
