using PubEntryLibrary.Data;
using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;
using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

using PubReport.Services;


namespace PubReport;

public partial class MainPage : ContentPage
{
	#region Constructor
	public MainPage()
	{
		InitializeComponent();
		LoadData();
	}

	private async void LoadData()
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

		locationPicker.ItemsSource = (await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location)).ToList();
		locationPicker.ItemDisplayBinding = new Binding(nameof(LocationModel.Name));
		locationPicker.SelectedIndex = 0;
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
		else await PrintPDF();
	}

	private async void DetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else await PrintPDF(true);
	}

	private async void ExcelDetailReportButtonClicked(object sender, EventArgs e)
	{
		if (!ValidateTime()) await DisplayAlert("Alert", "Incorrect Time or Date", "OK");
		else await ExportToExcel();
	}
	#endregion

	private async Task PrintPDF(bool isDetail = false)
	{
		MemoryStream ms;
		if (isDetail) ms = await PDF.Detail(fromDatePicker.Date.Add(fromTimePicker.Time), toDatePicker.Date.Add(toTimePicker.Time), (locationPicker.SelectedItem as LocationModel).Id);
		else ms = await PDF.Summary(fromDatePicker.Date.Add(fromTimePicker.Time), toDatePicker.Date.Add(toTimePicker.Time));

		SaveService saveService = new();
		if (isDetail) saveService.SaveAndView("DetailReport.pdf", "application/pdf", ms);
		else saveService.SaveAndView("SummaryReport.pdf", "application/pdf", ms);
	}

	private async Task ExportToExcel()
	{
		MemoryStream ms = await Excel.TransactionAdvanceExcel(fromDatePicker.Date.Add(fromTimePicker.Time), toDatePicker.Date.Add(toTimePicker.Time), (locationPicker.SelectedItem as LocationModel).Id);
		SaveService saveService = new();
		saveService.SaveAndView("DetailedExcel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}
}
