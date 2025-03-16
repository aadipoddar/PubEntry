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
/// Interaction logic for DetailedReportWindow.xaml
/// </summary>
public partial class DetailedReportWindow : Window
{
	private static int PubOpenTime => (int)Application.Current.Resources[SettingsKeys.PubOpenTime];
	private static int PubCloseTime => (int)Application.Current.Resources[SettingsKeys.PubCloseTime];
	private static int RefreshReportTimer => (int)Application.Current.Resources[SettingsKeys.RefreshReportTimer];

	private static DateTime _fromDateTime, _toDateTime;
	private static int _locationId;

	public DetailedReportWindow(DateTime fromDateTime, DateTime toDateTime, int locationId)
	{
		InitializeComponent();
		_fromDateTime = fromDateTime;
		_toDateTime = toDateTime;
		_locationId = locationId;
	}

	public DetailedReportWindow(DateTime fromDateTime, DateTime toDateTime)
	{
		InitializeComponent();
		_fromDateTime = fromDateTime;
		_toDateTime = toDateTime;
		_locationId = 0;
	}

	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadComboBox();
		await LoadDateTime();
		InitializeTimer();
	}

	#region LoadData

	private async Task LoadComboBox()
	{
		#region LoadTime

		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
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

		fromDatePicker.SelectedDate = _fromDateTime.Date;
		toDatePicker.SelectedDate = _toDateTime.Date;

		fromTimePicker.SelectedItem = _fromDateTime.Hour >= 12 ? _fromDateTime.Hour - 12 : _fromDateTime.Hour;
		toTimePicker.SelectedItem = _toDateTime.Hour >= 12 ? _toDateTime.Hour - 12 : _toDateTime.Hour;

		fromSlotPicker.SelectedItem = _fromDateTime.Hour >= 12 ? "PM" : "AM";
		toSlotPicker.SelectedItem = _toDateTime.Hour >= 12 ? "PM" : "AM";

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;

		#endregion

		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);

		if (_locationId > 0) locationComboBox.SelectedValue = _locationId;
		else locationComboBox.SelectedIndex = 0;

		if (ActualWidth > 1900)
		{
			transactionDataGrid.MinColumnWidth = 100;
			advanceDataGrid.MinColumnWidth = 100;
		}
	}

	private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(RefreshReportTimer) };

	private void InitializeTimer()
	{
		_timer.Tick += async (sender, e) => await LoadData();
		_timer.Start();
	}

	private async void values_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadDateTime();

	private async void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadDateTime();

	private async void RefreshData(object sender, ExecutedRoutedEventArgs e) => await LoadData();

	private async Task LoadDateTime()
	{
		if (toDatePicker.SelectedDate is null ||
			fromDatePicker.SelectedDate is null ||
			fromTimePicker.SelectedItem is null ||
			toTimePicker.SelectedItem is null ||
			toTimePicker is null ||
			fromTimePicker is null ||
			toSlotPicker is null ||
			fromSlotPicker is null ||
			locationComboBox.SelectedValue is null) return;

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;

		var fromTime = fromSlotPicker.SelectedItem.ToString() == "AM" ? (int)fromTimePicker.SelectedItem : (int)fromTimePicker.SelectedItem + 12;
		var toTime = toSlotPicker.SelectedItem.ToString() == "AM" ? (int)toTimePicker.SelectedItem : (int)toTimePicker.SelectedItem + 12;

		_fromDateTime = fromDatePicker.SelectedDate.Value.AddHours(fromTime);
		_toDateTime = toDatePicker.SelectedDate.Value.AddHours(toTime);

		_locationId = (int)locationComboBox.SelectedValue;

		Title = $"{(locationComboBox.SelectedItem as LocationModel).Name} Detailed Report - {_fromDateTime:dd/MM/yy h tt} to {_toDateTime:dd/MM/yy h tt}";

		await LoadData();
	}

	private async Task LoadData()
	{
		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(_fromDateTime, _toDateTime, _locationId);

		var detailedAdvancePrintModel = _toDateTime.TimeOfDay < TimeSpan.FromHours(17)
			? await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), _locationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.Date, _locationId);


		transactionDataGrid.ItemsSource = detailedTransactionPrintModel;
		foreach (DataGridColumn column in transactionDataGrid.Columns)
		{
			if (new[] { 1, 2 }.Contains(column.DisplayIndex))
				column.Visibility = Visibility.Collapsed;

			if (new[] { 0, 7, 8, 9, 10, 11, 12 }.Contains(column.DisplayIndex))
				column.CellStyle = new Style(typeof(DataGridCell)) { Setters = { new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right) } };
		}

		advanceDataGrid.ItemsSource = detailedAdvancePrintModel;
		foreach (DataGridColumn column in advanceDataGrid.Columns)
			if (new[] { 0, 8, 9, 11, 12, 14 }.Contains(column.DisplayIndex))
				column.CellStyle = new Style(typeof(DataGridCell)) { Setters = { new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right) } };


		peopleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Male + x.Female)}";
		maleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Male)}";
		femaleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Female)}";
		loyaltyTextBox.Text = $"{detailedTransactionPrintModel.Count(x => x?.Loyalty == 'L')}";

		amountTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Cash + x.Card + x.UPI + x.Amex)}";
		cashTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Cash)}";
		cardTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Card)}";
		upiTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.UPI)}";
		amexTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Amex)}";

		advanceTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x?.Amount)}";
		redeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId != "NOT REDEEMED").Sum(x => x?.Amount)}";
		notRedeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId == "NOT REDEEMED").Sum(x => x?.Amount)}";

		bookingTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x?.Booking)}";
		redeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId != "NOT REDEEMED").Sum(x => x?.Booking)}";
		notRedeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId == "NOT REDEEMED").Sum(x => x?.Booking)}";
	}

	#endregion

	private async void PrintPDF(object sender, ExecutedRoutedEventArgs e)
	{
		MemoryStream ms = await PDF.Detail(_fromDateTime, _toDateTime, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.pdf") { UseShellExecute = true });
	}

	private async void ExportExcel(object sender, ExecutedRoutedEventArgs e)
	{
		MemoryStream ms = await Excel.TransactionAdvanceExcel(_fromDateTime, _toDateTime, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReport.xlsx"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.xlsx") { UseShellExecute = true });
	}

	private void transactionDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
	{
		if (transactionDataGrid.SelectedItem is TransactionPrintModel) loadTransactionButton.Visibility = Visibility.Visible;
		else loadTransactionButton.Visibility = Visibility.Collapsed;
	}

	private void advanceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
	{
		if (advanceDataGrid.SelectedItem is AdvancePrintModel selectedAdvance)
		{
			transactionDataGrid.SelectedItem = null;
			if (selectedAdvance.SlipId != "NOT REDEEMED")
			{
				var transaction = transactionDataGrid.Items
					.OfType<TransactionPrintModel>()
					.FirstOrDefault(t => t.Id == int.Parse(selectedAdvance.SlipId));
				if (transaction != null)
				{
					transactionDataGrid.ScrollIntoView(transaction);
					transactionDataGrid.SelectedItem = transaction;
				}
			}
		}
	}

	private void loadTransactionButton_Click(object sender, RoutedEventArgs e)
	{
	}
}
