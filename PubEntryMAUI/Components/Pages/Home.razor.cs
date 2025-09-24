using System.Reflection;

using Microsoft.AspNetCore.Components;

using PubEntryLibrary.Data;
using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;
using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

using PubEntryMAUI.Services;

#if ANDROID
using PubEntryMAUI.Services.Android;
#endif

using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Popups;

namespace PubEntryMAUI.Components.Pages;

public partial class Home
{
	[Inject] private NavigationManager NavManager { get; set; }

	private DateTime _fromDateTime = DateTime.Now;
	private DateTime _toDateTime = DateTime.Now;
	private DateTime _advanceDate = DateTime.Now;

	private int _selectedLocationId;

	private List<LocationModel> _locations = [];
	private readonly List<TransactionTotalsModel> _transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> _advanceTotalsModel = [];

	#region Updating
	private string _updatingText = "Loading...";
	private bool _isLoadingDialogVisible = false;

	// Added missing fields for update dialog
	private DateTime _updateStartTime;
	private string _currentFunFact = "";
	private string _isLoadingText = "Loading...";
	private int _progressPercentage = 0;
	private string _estimatedTime = "";

	// Fun facts array for display during updates
	private readonly string[] _funFacts = [
		"Did you know? The first computer bug was an actual bug found in 1947!",
		"Fun fact: The term 'debugging' was coined by Grace Hopper.",
		"Coffee fact: Programmers consume 12% of the world's coffee supply!",
		"Tech trivia: The first 1GB hard drive weighed over 500 pounds!",
		"Code wisdom: There are only 10 types of people: those who understand binary and those who don't."
	];

	private SfDialog _sfUpdateDialog;

#if ANDROID
	private async Task UpdateApplication()
	{
		try
		{
			var currentVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";

			if (await AadiSoftUpdater.CheckForUpdates("aadipoddar", "PubEntry", currentVersion))
			{
				_isLoadingDialogVisible = true;
				_updateStartTime = DateTime.Now;
				_currentFunFact = _funFacts[new Random().Next(_funFacts.Length)];
				_isLoadingText = "Updating application... 0%";
				_progressPercentage = 0;
				_estimatedTime = "Calculating...";
				StateHasChanged();

				var progress = new Progress<int>(percentage =>
				{
					_progressPercentage = percentage;
					_isLoadingText = $"Updating application... {percentage}%";

					// Calculate estimated time remaining
					if (percentage > 0)
					{
						var elapsed = DateTime.Now - _updateStartTime;
						var totalEstimated = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds * 100 / percentage);
						var remaining = totalEstimated - elapsed;

						if (remaining.TotalSeconds > 0)
						{
							_estimatedTime = remaining.TotalMinutes >= 1
								? $"~{remaining.Minutes}m {remaining.Seconds}s remaining"
								: $"~{remaining.Seconds}s remaining";
						}
						else
							_estimatedTime = "Almost done...";
					}

					InvokeAsync(StateHasChanged);
				});

				await AadiSoftUpdater.UpdateApp("aadipoddar", "PubEntry", "com.aadisoft.pubEntry", progress);

				// Hide dialog after update completes
				_isLoadingDialogVisible = false;
				StateHasChanged();
			}
		}
		catch (Exception ex)
		{
			_updatingText = "Please check your Internet Connection.";
			_isLoadingDialogVisible = false;
			StateHasChanged();
			// Optionally log the exception: Console.WriteLine($"Update error: {ex.Message}");
		}
	}
#endif
	#endregion

	#region Load Data
	protected override async Task OnInitializedAsync()
	{
#if ANDROID
		await UpdateApplication();
#endif
		await ValidateUser();
		await LoadData();
	}

	private async Task LoadData()
	{
		_locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		_selectedLocationId = _locations.FirstOrDefault()?.Id ?? 0;

		if (DateTime.Now.Hour >= int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)))
		{
			_toDateTime = DateTime.Now.Date.AddDays(1).AddHours(int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)));
			_fromDateTime = DateTime.Now.Date.AddHours(int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)));
			_advanceDate = DateTime.Now.Date;
		}
		else
		{
			_toDateTime = DateTime.Now.Date.AddHours(int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)));
			_fromDateTime = DateTime.Now.Date.AddDays(-1).AddHours(int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)));
			_advanceDate = DateTime.Now.Date.AddDays(-1);
		}

		await LoadTransactionsAdvance();
	}

	public async void OnFromValueChanged(ChangedEventArgs<DateTime> args)
	{
		_fromDateTime = args.Value;
		await LoadTransactionsAdvance();
		StateHasChanged();
	}

	public async void OnToValueChanged(ChangedEventArgs<DateTime> args)
	{
		_toDateTime = args.Value;
		await LoadTransactionsAdvance();
		StateHasChanged();
	}

	private async Task LoadTransactionsAdvance()
	{
		_transactionTotalsModel.Clear();
		_advanceTotalsModel.Clear();

		foreach (var location in _locations)
		{
			var transactionTotal = await TransactionData.LoadTransactionTotalsByDateLocation(_fromDateTime, _toDateTime, location.Id);
			if (transactionTotal is not null)
			{
				_transactionTotalsModel.Add(transactionTotal);
			}
			else
			{
				_transactionTotalsModel.Add(new TransactionTotalsModel { LocationId = location.Id });
			}

			var advanceTotal = _toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(_fromDateTime.Date, _toDateTime.Date, location.Id);

			if (advanceTotal is not null)
			{
				_advanceTotalsModel.Add(advanceTotal);
			}
			else
			{
				_advanceTotalsModel.Add(new AdvanceTotalsModel { LocationId = location.Id });
			}
		}

		StateHasChanged();
	}
	#endregion

	#region Update Dialog Methods
	public void ShowUpdateDialog()
	{
		_isLoadingDialogVisible = true;
		StateHasChanged();
	}

	public void HideUpdateDialog()
	{
		_isLoadingDialogVisible = false;
		StateHasChanged();
	}
	#endregion

	#region Exporting
	private async Task SummaryButtonClicked()
	{
		var ms = await PDF.Summary(_fromDateTime, _toDateTime);
		var fileName = $"SummaryReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task DetailedPDFButtonClicked()
	{
		var ms = await PDF.Detail(_fromDateTime, _toDateTime, _selectedLocationId);
		var locationName = _locations.FirstOrDefault(_ => _.Id == _selectedLocationId)?.Name ?? "Unknown";
		var fileName = $"DetailedPDFReport_{locationName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task DetailedExcelButtonClicked()
	{
		var ms = await Excel.TransactionAdvanceExcel(_fromDateTime, _toDateTime, _selectedLocationId);
		var locationName = _locations.FirstOrDefault(_ => _.Id == _selectedLocationId)?.Name ?? "Unknown";
		var fileName = $"DetailedExcelReport_{locationName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}

	private async Task AdvancePDFButtonClicked()
	{
		var ms = await PDF.AdvanceTakeOn(_advanceDate, _selectedLocationId);
		var locationName = _locations.FirstOrDefault(_ => _.Id == _selectedLocationId)?.Name ?? "Unknown";
		var fileName = $"AdvancePDFReport_{locationName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task AdvanceExcelButtonClicked()
	{
		var ms = await Excel.AdvanceTakeOnExcel(_advanceDate, _selectedLocationId);
		var locationName = _locations.FirstOrDefault(_ => _.Id == _selectedLocationId)?.Name ?? "Unknown";
		var fileName = $"AdvanceExcelReport_{locationName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}
	#endregion

	#region Authentication
	private void LogOut()
	{
		SecureStorage.Default.RemoveAll();
		NavManager.NavigateTo("/Login", true);
	}

	private async Task ValidateUser()
	{
		var userId = await SecureStorage.Default.GetAsync("UserId");
		var password = await SecureStorage.Default.GetAsync("Password");

		if (userId is null || password is null)
		{
			LogOut();
			return;
		}

		var user = await CommonData.LoadTableDataById<UserModel>(TableNames.User, int.Parse(userId));
		if (user is null || user.Password != password)
			LogOut();
	}
	#endregion
}