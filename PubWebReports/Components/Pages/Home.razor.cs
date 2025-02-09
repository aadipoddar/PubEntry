using Microsoft.JSInterop;

using Syncfusion.Blazor.Calendars;

namespace PubWebReports.Components.Pages;

public partial class Home
{
	private DateTime FromDateTime { get; set; } = DateTime.Now;
	private DateTime ToDateTime { get; set; } = DateTime.Now;

	private int selectedLocationId;

	private readonly List<LocationModel> locations = [];
	private readonly List<TransactionTotalsModel> transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		locations.Clear();
		var activeLocations = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location);
		foreach (var location in activeLocations)
			locations.Add(location);

		selectedLocationId = locations.FirstOrDefault().Id;

		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			ToDateTime = DateTime.Now.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			FromDateTime = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			ToDateTime = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			FromDateTime = DateTime.Now.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}

		await LoadTransactionsAdvance();
	}

	private async Task LoadTransactionsAdvance()
	{
		transactionTotalsModel.Clear();
		advanceTotalsModel.Clear();
		foreach (var location in locations)
		{
			transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(FromDateTime, ToDateTime, location.Id));
			if (transactionTotalsModel.LastOrDefault() is null)
			{
				transactionTotalsModel.Remove(transactionTotalsModel.LastOrDefault());
				transactionTotalsModel.Add(new TransactionTotalsModel());
				transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}

			advanceTotalsModel.Add(ToDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(FromDateTime.Date, ToDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(FromDateTime.Date, ToDateTime.Date, location.Id));
			if (advanceTotalsModel.LastOrDefault() is null)
			{
				advanceTotalsModel.Remove(advanceTotalsModel.LastOrDefault());
				advanceTotalsModel.Add(new AdvanceTotalsModel());
				advanceTotalsModel.LastOrDefault().LocationId = location.Id;
			}
		}
	}

	public async void OnFromValueChanged(ChangedEventArgs<DateTime> args)
	{
		FromDateTime = args.Value;
		await LoadTransactionsAdvance();
		StateHasChanged();
	}

	public async void OnToValueChanged(ChangedEventArgs<DateTime> args)
	{
		ToDateTime = args.Value;
		await LoadTransactionsAdvance();
		StateHasChanged();
	}

	private void OnDetailedClick(int locationId) =>
		NavManager.NavigateTo(
			$"/detailed-report" +
			$"?SelectedLocationId={locationId}" +
			$"&FromDateTime={FromDateTime}" +
			$"&ToDateTime={ToDateTime}");

	private async Task SummaryButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"SummaryReport.pdf",
			Convert.ToBase64String((await SummaryPrint.PrintSummary(FromDateTime, ToDateTime)).ToArray()));

	private async Task DetailedButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"DetailedReport.pdf",
			Convert.ToBase64String((await DetailPrint.PrintDetail(FromDateTime, ToDateTime, selectedLocationId)).ToArray()));

	private async Task ExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"ExcelReport.xlsx",
			Convert.ToBase64String((await Excel.ExcelExport(FromDateTime, ToDateTime, selectedLocationId)).ToArray()));
}