using Syncfusion.Blazor.Calendars;

namespace PubWebReport.Components.Pages;

public partial class Home
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private static DateTime CurrentDateTime { get; set; } = DateTime.Now.AddHours(5).AddMinutes(30);
	private DateTime FromDateTime { get; set; } = CurrentDateTime;
	private DateTime ToDateTime { get; set; } = CurrentDateTime;

	private string Password { get; set; }

	private int selectedLocationId;
	private int selectedUserId;

	private readonly List<LocationModel> locations = [];
	private readonly List<UserModel> users = [];
	private readonly List<TransactionTotalsModel> transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	protected override Task OnAfterRenderAsync(bool firstRender)
	{
		// TODO - Testing
		NavManager.NavigateTo(
				$"/admin" +
				$"?UserId={23}" +
				$"&Password={BCrypt.Net.BCrypt.EnhancedHashPassword("1234", 13)}");
		return base.OnAfterRenderAsync(firstRender);
	}

	private async Task LoadData()
	{
		locations.Clear();
		users.Clear();

		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location))
			locations.Add(location);

		foreach (var user in await CommonData.LoadTableDataByStatus<UserModel>(Table.User))
			if (user.Admin)
				users.Add(user);

		selectedLocationId = locations.FirstOrDefault().Id;
		selectedUserId = users.FirstOrDefault().Id;

		if (CurrentDateTime.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			ToDateTime = CurrentDateTime.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			FromDateTime = CurrentDateTime.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			ToDateTime = CurrentDateTime.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			FromDateTime = CurrentDateTime.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
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

	private async Task AdminButtonClicked()
	{
		if (await ValidatePassword())
			NavManager.NavigateTo(
				$"/admin" +
				$"?UserId={selectedUserId}" +
				$"&Password={BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13)}");

		else await JS.InvokeVoidAsync("alert", "Invalid Password");
	}

	private async Task<bool> ValidatePassword()
	{
		if (string.IsNullOrEmpty(Password)) return false;

		if ((await CommonData.LoadTableDataById<UserModel>(Table.User, selectedUserId)).Password == Password)
			return true;

		Password = string.Empty;
		return false;
	}
}