using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using System.Globalization;

namespace PubReportMobile;

public partial class MainPage : ContentPage
{
	LocationModel selectedLocation;
	int selectedLocationId;

	public MainPage()
	{
		InitializeComponent();

		LoadTextBoxes();
	}

	public DateTime FormDate { get; set; } = DateTime.Now;
	public DateTime ToDate { get; set; } = DateTime.Now;
	public int FromTime { get; set; } = 5;
	public int ToTime { get; set; } = 24;

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
			{
				selectedLocation = locations[locationPicker.SelectedIndex];
				selectedLocationId = selectedLocation.Id;
			}
		};
	}

	#region GetData
	private void GetDateTime(out string fromDateTime, out string toDateTime)
	{
		fromDateTime = fromDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		toDateTime = toDatePicker.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

		fromDateTime = fromDateTime + $" {fromTimePicker.Time}";
		toDateTime = toDateTime + $" {toTimePicker.Time}";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimePicker.Time}";

		else
			return toDatePicker.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimePicker.Time}";
	}

	private List<TransactionModel> GetTransactionsByLocationId(int locationId)
	{
		string fromDateTime, toDateTime;
		GetDateTime(out fromDateTime, out toDateTime);
		return Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}
	#endregion

	private void DetailReportButtonClicked(object sender, EventArgs e)
	{

	}

	private void SummaryReportButtonClicked(object sender, EventArgs e)
	{
		Print();
	}

	private async void Print()
	{

	}
}