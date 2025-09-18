#if ANDROID
using System.Reflection;

using PubReport.Platforms.Android;
#endif

using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

using PubReport.Services;

namespace PubReport;

public partial class MainPage : ContentPage
{
	private readonly IDispatcherTimer _refreshTimer;

	private static DateTime _fromDateTime, _toDateTime;
	private bool _isLoadingData;

	public MainPage()
	{
		InitializeComponent();
		_refreshTimer = Dispatcher.CreateTimer();
	}

	private async Task InitializeRefreshTimer()
	{
		_refreshTimer.Interval = TimeSpan.FromSeconds(int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.RefreshReportTimer)));
		_refreshTimer.Tick += async (sender, e) => await LoadData();
	}

	#region LoadData

	private async void ContentPage_Loaded(object sender, EventArgs e)
	{
		popup.StaysOpen = true;
		popup.Show();

#if ANDROID
		if (await AadiSoftUpdater.CheckForUpdates("aadipoddar", "PubEntry", Assembly.GetExecutingAssembly().GetName().Version.ToString()))
			await AadiSoftUpdater.UpdateApp("aadipoddar", "PubEntry", "com.aadisoft.pubreport");
#endif

		await InitializeRefreshTimer();

		await LoadComboBox();
		await LoadData();

		popup.StaysOpen = false;
		popup.Dismiss();
		popup.StaysOpen = true;

#if ANDROID
		await Permissions.RequestAsync<Permissions.PostNotifications>();
		if (!PubReport.Platforms.Android.AndroidServiceManager.IsRunning)
			PubReport.Platforms.Android.AndroidServiceManager.StartMyService();
#endif

		_refreshTimer.Start();
	}

	private async Task LoadComboBox()
	{
		_isLoadingData = true;

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

		locationPicker.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationPicker.ItemDisplayBinding = new Binding(nameof(LocationModel.Name));
		locationPicker.SelectedIndex = 0;

		advanceDatePicker.Date = DateTime.Now.Date;

		_isLoadingData = false;
	}

	private async void datePicker_DateSelected(object sender, DateChangedEventArgs e) =>
		await LoadData();

	private async void timePicker_TimeSelected(object sender, TimeChangedEventArgs e) =>
		await LoadData();

	private async void refreshButton_Clicked(object sender, EventArgs e) =>
		await LoadData();

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

	#endregion

	#region Button

	private async void summaryReportButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await PDF.Summary(_fromDateTime, _toDateTime);
		SaveService.SaveAndView("SummaryReport.pdf", "application/pdf", ms);
	}

	private async void detailedPDFButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await PDF.Detail(_fromDateTime, _toDateTime, (locationPicker.SelectedItem as LocationModel).Id);
		SaveService.SaveAndView("DetailedReport.pdf", "application/pdf", ms);
	}

	private async void detailedExcelButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await Excel.TransactionAdvanceExcel(_fromDateTime, _toDateTime, (locationPicker.SelectedItem as LocationModel).Id);
		SaveService.SaveAndView("DetailedReport.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}

	private async void advancePDFButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await PDF.AdvanceTakeOn(advanceDatePicker.Date, (locationPicker.SelectedItem as LocationModel).Id);
		SaveService.SaveAndView("AdvanceReport.pdf", "application/pdf", ms);
	}

	private async void advanceExcelButton_Clicked(object sender, EventArgs e)
	{
		MemoryStream ms = await Excel.AdvanceTakeOnExcel(advanceDatePicker.Date, (locationPicker.SelectedItem as LocationModel).Id);
		SaveService.SaveAndView("AdvanceReport.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}

	#endregion
}