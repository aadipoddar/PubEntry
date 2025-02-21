using Syncfusion.Blazor.Calendars;

namespace PubWebReport.Components.Pages;

public partial class Home
{
	[Inject] public NavigationManager NavManager { get; set; }
	[Inject] public IJSRuntime JS { get; set; }

	private DateTime CurrentDateTime { get; set; }
	private DateTime FromDateTime { get; set; }
	private DateTime ToDateTime { get; set; }

	private string Password { get; set; }

	private int selectedLocationId;
	private int selectedUserId;

	private readonly List<LocationModel> locations = [];
	private readonly List<UserModel> users = [];
	private readonly List<TransactionTotalsModel> transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		locations.Clear();
		users.Clear();

		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location))
			locations.Add(location);

		foreach (var user in await UserData.LoadUsersByLocationId(locations.FirstOrDefault().Id))
			users.Add(user);

		selectedLocationId = locations.FirstOrDefault().Id;
		selectedUserId = users.FirstOrDefault().Id;

		CurrentDateTime = DateTime.Now.AddHours(5).AddMinutes(30);

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
			Convert.ToBase64String((await PDF.Summary(FromDateTime, ToDateTime)).ToArray()));

	private async Task DetailedButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"DetailedReport.pdf",
			Convert.ToBase64String((await PDF.Detail(FromDateTime, ToDateTime, selectedLocationId)).ToArray()));

	private async Task ExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"ExcelReport.xlsx",
			Convert.ToBase64String((await Excel.TransactionAdvanceExcel(FromDateTime, ToDateTime, selectedLocationId)).ToArray()));

	private async Task OnLocationChanged()
	{
		users.Clear();
		foreach (var user in await UserData.LoadUsersByLocationId(selectedLocationId))
			users.Add(user);

		selectedUserId = users.FirstOrDefault().Id;
		StateHasChanged();
	}

	private async Task AdvanceButtonClicked()
	{
		if (await ValidatePassword())
			NavManager.NavigateTo(
				$"/advance" +
				$"?UserId={selectedUserId}" +
				$"&Password={BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13)}" +
				$"&LocationId={selectedLocationId}");

		else await JS.InvokeVoidAsync("alert", "Invalid Password");
	}

	private async Task AdminButtonClicked()
	{
		if ((await CommonData.LoadTableDataById<UserModel>(Table.User, selectedUserId)).Admin)
		{
			if (await ValidatePassword())
				NavManager.NavigateTo(
					$"/admin" +
					$"?UserId={selectedUserId}" +
					$"&Password={BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13)}");

			else await JS.InvokeVoidAsync("alert", "Invalid Password");
		}
		else await JS.InvokeVoidAsync("alert", "Not an Admin");
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