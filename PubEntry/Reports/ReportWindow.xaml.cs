using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using PubEntryLibrary.Printing.PDF;

namespace PubEntry.Reports;

/// <summary>
/// Interaction logic for ReportWindow.xaml
/// </summary>
public partial class ReportWindow : Window
{
	private static int PubOpenTime => (int)Application.Current.Resources[SettingsKeys.PubOpenTime];
	private static int PubCloseTime => (int)Application.Current.Resources[SettingsKeys.PubCloseTime];
	private static int RefreshReportTimer => (int)Application.Current.Resources[SettingsKeys.RefreshReportTimer];

	private static DateTime _fromDateTime, _toDateTime;

	public ReportWindow() => InitializeComponent();

	#region LoadData

	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		LoadComboBox();
		await LoadData(true);
		InitializeTimer();
	}

	private void LoadComboBox()
	{
		if (DateTime.Now.Hour >= PubOpenTime)
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddDays(1);
			fromDatePicker.SelectedDate = DateTime.Now.Date;
		}
		else
		{
			toDatePicker.SelectedDate = DateTime.Now.Date;
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddDays(-1);
		}

		List<int> hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
		List<string> slots = ["AM", "PM"];

		fromTimePicker.ItemsSource = hours;
		toTimePicker.ItemsSource = hours;
		fromSlotPicker.ItemsSource = slots;
		toSlotPicker.ItemsSource = slots;

		if (PubOpenTime >= 12)
		{
			fromSlotPicker.SelectedItem = "PM";
			if (PubOpenTime > 12) fromTimePicker.SelectedItem = PubOpenTime - 12;
			else fromTimePicker.SelectedItem = PubOpenTime;
		}

		else
		{
			fromSlotPicker.SelectedItem = "AM";
			fromTimePicker.SelectedItem = PubOpenTime;
		}

		if (PubCloseTime >= 12)
		{
			toSlotPicker.SelectedItem = "PM";
			if (PubCloseTime > 12) toTimePicker.SelectedItem = PubCloseTime - 12;
			else toTimePicker.SelectedItem = PubCloseTime;
		}
		else
		{
			toSlotPicker.SelectedItem = "AM";
			toTimePicker.SelectedItem = PubCloseTime;
		}

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;
	}

	private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(RefreshReportTimer) };

	private void InitializeTimer()
	{
		_timer.Tick += async (sender, e) => await LoadData();
		_timer.Start();
	}

	private async void values_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadData();

	private async Task LoadData(bool initialLoad = false)
	{
		if (toDatePicker.SelectedDate is null ||
			fromDatePicker.SelectedDate is null ||
			fromTimePicker.SelectedItem is null ||
			toTimePicker.SelectedItem is null ||
			toTimePicker is null ||
			fromTimePicker is null ||
			toSlotPicker is null ||
			fromSlotPicker is null) return;

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;

		var fromTime = fromSlotPicker.SelectedItem.ToString() == "AM" ? (int)fromTimePicker.SelectedItem : (int)fromTimePicker.SelectedItem + 12;
		var toTime = toSlotPicker.SelectedItem.ToString() == "AM" ? (int)toTimePicker.SelectedItem : (int)toTimePicker.SelectedItem + 12;

		_fromDateTime = fromDatePicker.SelectedDate.Value.AddHours(fromTime);
		_toDateTime = toDatePicker.SelectedDate.Value.AddHours(toTime);

		await CreateExpanders.LoadExpandersData(_fromDateTime, _toDateTime, expanderGrid, initialLoad);
	}

	#endregion

	private async void RefreshData(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) => await LoadData();

	private async void PrintPDF(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
	{
		MemoryStream ms = await PDF.Summary(_fromDateTime, _toDateTime);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "SummaryReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\SummaryReport.pdf") { UseShellExecute = true });
	}

	private void DetailedReport(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
	{
		DetailedReportWindow detailedReportWindow = new(_fromDateTime, _toDateTime);
		detailedReportWindow.Show();
	}

	private void AdvanceReport(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
	{
		AdvanceReportWindow advanceReportWindow = new();
		advanceReportWindow.Show();
	}
}
