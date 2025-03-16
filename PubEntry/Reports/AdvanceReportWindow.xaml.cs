using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

namespace PubEntry.Reports;

/// <summary>
/// Interaction logic for AdvanceReportWindow.xaml
/// </summary>
public partial class AdvanceReportWindow : Window
{
	private static int RefreshReportTimer => (int)Application.Current.Resources[SettingsKeys.RefreshReportTimer];

	public AdvanceReportWindow() => InitializeComponent();

	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadComboBox();
		await LoadData();
		InitializeTimer();
	}

	#region LoadData

	private async Task LoadComboBox()
	{
		takenOnDatePicker.SelectedDate = DateTime.Now;

		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		locationComboBox.SelectedIndex = 0;

		if (ActualWidth > 1900) advanceDataGrid.MinColumnWidth = 100;
	}

	private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(RefreshReportTimer) };

	private void InitializeTimer()
	{
		_timer.Tick += async (sender, e) => await LoadData();
		_timer.Start();
	}

	private async void values_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadData();

	private async void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadData();

	private async void RefreshData(object sender, ExecutedRoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		if (takenOnDatePicker.SelectedDate is null ||
			locationComboBox.SelectedValue is null) return;

		Title = $"{(locationComboBox.SelectedItem as LocationModel).Name} Advance Report - {takenOnDatePicker.DisplayDate:dd/MM/yy}";

		advanceDataGrid.ItemsSource = await AdvanceData.LoadAdvancesByTakenOnLocation(takenOnDatePicker.SelectedDate.Value, (int)locationComboBox.SelectedValue);
		foreach (DataGridColumn column in advanceDataGrid.Columns)
			if (new[] { 0, 8, 9, 11, 12, 14 }.Contains(column.DisplayIndex))
				column.CellStyle = new Style(typeof(DataGridCell)) { Setters = { new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right) } };

		await CreatePaymentModeComponents();
	}

	private async Task CreatePaymentModeComponents()
	{
		paymentModeGrid.Children.Clear();
		paymentModeGrid.RowDefinitions.Clear();

		var paymentMode = await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(takenOnDatePicker.SelectedDate.Value, (int)locationComboBox.SelectedValue);

		for (int i = 0; i < paymentMode.Count; i++)
		{
			paymentModeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			var textBlock = new TextBlock
			{
				Text = paymentMode[i].PaymentMode,
				Margin = new Thickness(10),
				Padding = new Thickness(5)
			};
			Grid.SetRow(textBlock, i);
			Grid.SetColumn(textBlock, 0);
			paymentModeGrid.Children.Add(textBlock);

			var textBlock1 = new TextBox
			{
				Text = paymentMode[i].Amount.ToString(),
				Margin = new Thickness(10),
				Padding = new Thickness(5),
				IsReadOnly = true,
				MinWidth = 100,
				TextAlignment = TextAlignment.Right
			};
			Grid.SetRow(textBlock1, i);
			Grid.SetColumn(textBlock1, 1);
			paymentModeGrid.Children.Add(textBlock1);
		}
	}

	#endregion

	private async void PrintPDF(object sender, ExecutedRoutedEventArgs e)
	{
		MemoryStream ms = await PDF.AdvanceTakeOn(takenOnDatePicker.SelectedDate.Value, (int)locationComboBox.SelectedValue);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "AdvanceTakeOnReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\AdvanceTakeOnReport.pdf") { UseShellExecute = true });
	}

	private async void ExportExcel(object sender, ExecutedRoutedEventArgs e)
	{
		MemoryStream ms = await Excel.AdvanceTakeOnExcel(takenOnDatePicker.SelectedDate.Value, (int)locationComboBox.SelectedValue);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "AdvanceTakeOnReport.xlsx"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\AdvanceTakeOnReport.xlsx") { UseShellExecute = true });
	}
}