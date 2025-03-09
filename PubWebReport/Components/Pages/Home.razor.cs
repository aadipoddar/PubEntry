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

	private int _selectedLocationId;
	private int _selectedUserId;

	private readonly List<LocationModel> _locations = [];
	private readonly List<UserModel> _users = [];
	private readonly List<TransactionTotalsModel> _transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> _advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		_locations.Clear();
		_users.Clear();

		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location))
			_locations.Add(location);

		foreach (var user in await UserData.LoadUsersByLocationId(_locations.FirstOrDefault().Id))
			_users.Add(user);

		_selectedLocationId = _locations.FirstOrDefault().Id;
		_selectedUserId = _users.FirstOrDefault().Id;

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
		_transactionTotalsModel.Clear();
		_advanceTotalsModel.Clear();

		foreach (var location in _locations)
		{
			_transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(FromDateTime, ToDateTime, location.Id));
			if (_transactionTotalsModel.LastOrDefault() is null)
			{
				_transactionTotalsModel.Remove(_transactionTotalsModel.LastOrDefault());
				_transactionTotalsModel.Add(new TransactionTotalsModel());
				_transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}

			_advanceTotalsModel.Add(ToDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(FromDateTime.Date, ToDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(FromDateTime.Date, ToDateTime.Date, location.Id));
			if (_advanceTotalsModel.LastOrDefault() is null)
			{
				_advanceTotalsModel.Remove(_advanceTotalsModel.LastOrDefault());
				_advanceTotalsModel.Add(new AdvanceTotalsModel());
				_advanceTotalsModel.LastOrDefault().LocationId = location.Id;
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
			Convert.ToBase64String((await PDF.Detail(FromDateTime, ToDateTime, _selectedLocationId)).ToArray()));

	private async Task ExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"ExcelReport.xlsx",
			Convert.ToBase64String((await Excel.TransactionAdvanceExcel(FromDateTime, ToDateTime, _selectedLocationId)).ToArray()));

	private async Task OnLocationChanged()
	{
		_users.Clear();
		foreach (var user in await UserData.LoadUsersByLocationId(_selectedLocationId))
			_users.Add(user);

		_selectedUserId = _users.FirstOrDefault().Id;
		StateHasChanged();
	}

	private async Task AdvanceButtonClicked()
	{
		if (await ValidatePassword())
		{
			await JS.InvokeVoidAsync("setCookie", "AdvanceUserId", _selectedUserId, 1);
			await JS.InvokeVoidAsync("setCookie", "AdvancePassword", BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13), 1);
			await JS.InvokeVoidAsync("setCookie", "AdvanceLocationId", _selectedLocationId, 1);
			NavManager.NavigateTo("/advance");
		}

		else await JS.InvokeVoidAsync("alert", "Invalid Password");
	}

	private async Task AdminButtonClicked()
	{
		if ((await CommonData.LoadTableDataById<UserModel>(TableNames.User, _selectedUserId)).Admin)
		{
			if (await ValidatePassword())
			{
				await JS.InvokeVoidAsync("setCookie", "UserId", _selectedUserId, 1);
				await JS.InvokeVoidAsync("setCookie", "Password", BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 13), 1);
				NavManager.NavigateTo("/admin");
			}

			else await JS.InvokeVoidAsync("alert", "Invalid Password");
		}
		else await JS.InvokeVoidAsync("alert", "Not an Admin");
	}

	private async Task<bool> ValidatePassword()
	{
		if (string.IsNullOrEmpty(Password)) return false;

		if ((await CommonData.LoadTableDataById<UserModel>(TableNames.User, _selectedUserId)).Password == Password)
			return true;

		Password = string.Empty;
		return false;
	}
}