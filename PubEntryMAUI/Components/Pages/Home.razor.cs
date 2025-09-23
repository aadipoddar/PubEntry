using PubEntryLibrary.Data;
using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;
using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

using PubEntryMAUI.Services;

using Syncfusion.Blazor.Calendars;

namespace PubEntryMAUI.Components.Pages;

public partial class Home
{
	private DateTime _fromDateTime = DateTime.Now;
	private DateTime _toDateTime = DateTime.Now;
	private DateTime _advanceDate = DateTime.Now;

	private int _selectedLocationId;

	private List<LocationModel> _locations = [];
	private readonly List<TransactionTotalsModel> _transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> _advanceTotalsModel = [];

	#region Load Data
	protected override async Task OnInitializedAsync()
	{
		await LoadData();
		await base.OnInitializedAsync();
	}

	private async Task LoadData()
	{
		_locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		_selectedLocationId = _locations.FirstOrDefault()?.Id ?? 0;

		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
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
			_transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(_fromDateTime, _toDateTime, location.Id));
			if (_transactionTotalsModel.LastOrDefault() is null)
			{
				_transactionTotalsModel.Remove(_transactionTotalsModel.LastOrDefault());
				_transactionTotalsModel.Add(new TransactionTotalsModel());
				_transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}

			_advanceTotalsModel.Add(_toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(_fromDateTime.Date, _toDateTime.Date, location.Id));
			if (_advanceTotalsModel.LastOrDefault() is null)
			{
				_advanceTotalsModel.Remove(_advanceTotalsModel.LastOrDefault());
				_advanceTotalsModel.Add(new AdvanceTotalsModel());
				_advanceTotalsModel.LastOrDefault().LocationId = location.Id;
			}
		}

		StateHasChanged();
	}
	#endregion

	#region Exporting
	private async Task SummaryButtonClicked()
	{
		var ms = await PDF.Summary(_fromDateTime, _toDateTime);
		var fileName = $"SummaryReport_{DateTime.Now}.pdf";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task DetailedPDFButtonClicked()
	{
		var ms = await PDF.Detail(_fromDateTime, _toDateTime, _selectedLocationId);
		var fileName = $"DetailedPDFReport_{_locations.FirstOrDefault(_ => _.Id == _selectedLocationId).Name}_{DateTime.Now}.pdf";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task DetailedExcelButtonClicked()
	{
		var ms = await Excel.TransactionAdvanceExcel(_fromDateTime, _toDateTime, _selectedLocationId);
		var fileName = $"DetailedExcelReport_{_locations.FirstOrDefault(_ => _.Id == _selectedLocationId).Name}_{DateTime.Now}.xlsx";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}

	private async Task AdvancePDFButtonClicked()
	{
		var ms = await PDF.AdvanceTakeOn(_advanceDate, _selectedLocationId);
		var fileName = $"AdvancePDFReport_{_locations.FirstOrDefault(_ => _.Id == _selectedLocationId).Name}_{DateTime.Now}.xlsx";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/pdf", ms);
	}

	private async Task AdvanceExcelButtonClicked()
	{
		var ms = await Excel.AdvanceTakeOnExcel(_advanceDate, _selectedLocationId);
		var fileName = $"AdvanceExcelReport_{_locations.FirstOrDefault(_ => _.Id == _selectedLocationId).Name}_{DateTime.Now}.xlsx";
		SaveService saveService = new();
		saveService.SaveAndView(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ms);
	}
	#endregion
}