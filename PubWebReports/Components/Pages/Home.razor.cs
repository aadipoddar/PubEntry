using Microsoft.JSInterop;

using Syncfusion.Blazor.Calendars;

namespace PubWebReports.Components.Pages;

public partial class Home
{
	private DateTime fromDateTime { get; set; } = DateTime.Now;
	private DateTime toDateTime { get; set; } = DateTime.Now;

	private List<LocationModel> locations = [];
	private int selectedLocationId = 1;
	List<TransactionTotalsModel> transactionTotalsModel = [];
	List<AdvanceTotalsModel> advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		locations = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location);
		selectedLocationId = locations.FirstOrDefault().Id;

		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			toDateTime = DateTime.Now.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDateTime = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			toDateTime = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDateTime = DateTime.Now.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}

		await LoadTransactionsAdvance();
	}

	private async Task LoadTransactionsAdvance()
	{
		transactionTotalsModel.Clear();
		advanceTotalsModel.Clear();
		foreach (var location in locations)
		{
			transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(fromDateTime, toDateTime, location.Id));
			if (transactionTotalsModel.LastOrDefault() is null)
			{
				transactionTotalsModel.Remove(transactionTotalsModel.LastOrDefault());
				transactionTotalsModel.Add(new TransactionTotalsModel());
				transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}

			advanceTotalsModel.Add(toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.Date, location.Id));
			if (advanceTotalsModel.LastOrDefault() is null)
			{
				advanceTotalsModel.Remove(advanceTotalsModel.LastOrDefault());
				advanceTotalsModel.Add(new AdvanceTotalsModel());
				advanceTotalsModel.LastOrDefault().LocationId = location.Id;
			}
		}
	}

	private async Task SummaryButtonClicked()
	{
		if (!ValidateTime())
		{
			await JSRuntime.InvokeVoidAsync("alert", ["Incorrect Time"]);
			return;
		}

		MemoryStream ms = await SummaryPrint.PrintSummary(fromDateTime, toDateTime);
		await JS.InvokeVoidAsync("saveAsFile", "SummaryReport.pdf", Convert.ToBase64String(ms.ToArray()));
	}

	private async Task DetailedButtonClicked()
	{
		if (!ValidateTime())
		{
			await JSRuntime.InvokeVoidAsync("alert", ["Incorrect Time"]);
			return;
		}

		MemoryStream ms = await DetailPrint.PrintDetail(fromDateTime, toDateTime, selectedLocationId);
		await JS.InvokeVoidAsync("saveAsFile", "DetailedReport.pdf", Convert.ToBase64String(ms.ToArray()));
	}

	private async Task ExcelButtonClicked()
	{
		if (!ValidateTime())
		{
			await JSRuntime.InvokeVoidAsync("alert", ["Incorrect Time"]);
			return;
		}

		MemoryStream ms = await Excel.ExcelExport(fromDateTime, toDateTime, selectedLocationId);
		await JS.InvokeVoidAsync("saveAsFile", "ExcelReport.xlsx", Convert.ToBase64String(ms.ToArray()));
	}

	private bool ValidateTime() => fromDateTime < toDateTime;

	public async void OnFromValueChanged(ChangedEventArgs<DateTime> args)
	{
		fromDateTime = args.Value;
		await LoadTransactionsAdvance();
	}

	public async void OnToValueChanged(ChangedEventArgs<DateTime> args)
	{
		toDateTime = args.Value;
		await LoadTransactionsAdvance();
	}
}