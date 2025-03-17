using PubEntryLibrary.Printing.PDF;

using PubReport.Services;

namespace PubReport;

public partial class MainPage : ContentPage
{
	private static DateTime _fromDateTime, _toDateTime;
	private bool _isLoadingData;

	public MainPage() => InitializeComponent();

	private async void ContentPage_Loaded(object sender, EventArgs e)
	{
		await LoadComboBox();
		await LoadData();
	}

	private async Task LoadComboBox()
	{
		var openTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime));
		var closeTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime));

		if (DateTime.Now.Hour >= openTime)
		{
			fromDatePicker.Date = DateTime.Now.Date;
			toDatePicker.Date = DateTime.Now.Date.AddDays(1);
		}
		else
		{
			fromDatePicker.Date = DateTime.Now.Date.AddDays(-1);
			toDatePicker.Date = DateTime.Now.Date;
		}

		fromTimePicker.Time = TimeSpan.FromHours(openTime);
		toTimePicker.Time = TimeSpan.FromHours(closeTime);
	}

	private async void datePicker_DateSelected(object sender, DateChangedEventArgs e) => await LoadData();

	private async void timePicker_TimeSelected(object sender, TimeChangedEventArgs e) => await LoadData();

	private async void refreshButton_Clicked(object sender, EventArgs e) => await LoadData();

	private async void summaryReportButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await PDF.Summary(_fromDateTime, _toDateTime);
		SaveService saveService = new();
		saveService.SaveAndView("SummaryReport.pdf", "application/pdf", ms);
	}

	private async Task LoadData()
	{
		if (_isLoadingData) return;

		_isLoadingData = true;

		fromDatePicker.MaximumDate = toDatePicker.Date;
		toDatePicker.MinimumDate = fromDatePicker.Date;

		_fromDateTime = fromDatePicker.Date.AddHours(fromTimePicker.Time.Hours);
		_toDateTime = toDatePicker.Date.AddHours(fromTimePicker.Time.Hours);

		await CreateExpanders.LoadExpandersData(_fromDateTime, _toDateTime, expanderGrid);

		_isLoadingData = false;
	}
}
