using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;
using PubEntryLibrary.Printing;

using PubReport.Services;


namespace PubReport;

public partial class MainPage : ContentPage
{
	#region Constructor
	int selectedLocationId = 0;

	public MainPage()
	{
		InitializeComponent();
		LoadTextBoxes();
	}

	private void LoadTextBoxes()
	{
		if (DateTime.Now.Hour > 4)
			fromTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()) - 3);

		else fromTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()));

		toTimePicker.Time = TimeSpan.FromHours(int.Parse(DateTime.Now.Hour.ToString()));

		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		locationPicker.ItemsSource = locations;
		locationPicker.ItemDisplayBinding = new Binding(nameof(LocationModel.Name));
		locationPicker.SelectedIndex = 0;

		locationPicker.SelectedIndexChanged += (sender, args) =>
		{
			if (locationPicker.SelectedIndex != -1)
				selectedLocationId = locationPicker.SelectedIndex;
		};
	}
	#endregion

	private bool ValidateTime()
	{
		if (fromDatePicker.Date > toDatePicker.Date) return false;

		if (fromTimePicker.Time > toTimePicker.Time)
			if (fromDatePicker.Date == toDatePicker.Date)
				return false;

		return true;
	}

	#region Events
	private async void SummaryReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else
		{
			busyIndicator.IsVisible = true;
			await Task.Run(() => { PrintPDF(); });
			busyIndicator.IsVisible = false;
		}
	}

	private async void DetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else
		{
			busyIndicator.IsVisible = true;
			await Task.Run(() => { PrintPDF(true); });
			busyIndicator.IsVisible = false;
		}
	}
	#endregion

	#region GetData
	private string GetFromDateTime()
	{
		string fromDateTime = fromDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		return fromDateTime + $" {fromTimePicker.Time}";
	}

	private string GetToDateTime()
	{
		string toDateTime = toDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		return toDateTime + $" {toTimePicker.Time}";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimePicker.Time}";

		else
			return toDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimePicker.Time}";
	}
	#endregion

	void PrintPDF(bool isDetail = false)
	{
		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms;
		if (isDetail) ms = Printing.PrintDetail(dateHeader, fromTime, toTime, selectedLocationId + 1);
		else ms = Printing.PrintSummary(dateHeader, fromTime, toTime);

		SaveService saveService = new();
		if (isDetail) saveService.SaveAndView("DetailReport.pdf", "application/pdf", ms);
		else saveService.SaveAndView("SummaryReport.pdf", "application/pdf", ms);
	}

	private async void ExcelDetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else
		{
			busyIndicator.IsVisible = true;
			await Task.Run(() => { ExportToExcel();  });
			busyIndicator.IsVisible = false;
		}
	}

	private void ExportToExcel()
	{
		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms = Excel.ExcelExport(dateHeader, fromTime, toTime, selectedLocationId + 1);
		SaveService saveService = new SaveService();
		saveService.SaveAndView("DetailedExcel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}
}
