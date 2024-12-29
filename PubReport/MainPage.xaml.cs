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

	private async void LoadTextBoxes()
	{
		if (DateTime.Now.Hour >= 17)
		{
			toDatePicker.Date = DateTime.Now.Date.AddDays(1);
			fromDatePicker.Date = DateTime.Now.Date;
		}
		else
		{
			toDatePicker.Date = DateTime.Now.Date;
			fromDatePicker.Date = DateTime.Now.Date.AddDays(-1);
		}

		locationPicker.ItemsSource = (await LocationData.LoadActiveLocations()).ToList();
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
		else PrintPDF();
	}

	private async void DetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else PrintPDF(true);
	}

	private async void ExcelDetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else ExportToExcel();
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
		if (getFromDate) return fromDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimePicker.Time}";
		else return toDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimePicker.Time}";
	}
	#endregion

	private async void PrintPDF(bool isDetail = false)
	{
		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms;
		if (isDetail) ms = await PrintReport.PrintDetail(dateHeader, fromTime, toTime, selectedLocationId + 1);
		else ms = await PrintReport.PrintSummary(dateHeader, fromTime, toTime);

		SaveService saveService = new();
		if (isDetail) saveService.SaveAndView("DetailReport.pdf", "application/pdf", ms);
		else saveService.SaveAndView("SummaryReport.pdf", "application/pdf", ms);
	}

	private async void ExportToExcel()
	{
		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms = await Excel.ExcelExport(dateHeader, fromTime, toTime, selectedLocationId + 1);
		SaveService saveService = new();
		saveService.SaveAndView("DetailedExcel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}
}
