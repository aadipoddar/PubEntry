using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;

namespace PubWebReport.Components.Pages;

public partial class Detailed
{
	[Inject] public IJSRuntime JS { get; set; }

	[Parameter][SupplyParameterFromQuery] public int SelectedLocationId { get; set; }
	[Parameter][SupplyParameterFromQuery] public DateTime FromDateTime { get; set; }
	[Parameter][SupplyParameterFromQuery] public DateTime ToDateTime { get; set; }
	[Parameter][SupplyParameterFromQuery] public DateTime TakenOnDate { get; set; }

	private DateTime CurrentDateTime { get; set; }

	private SfGrid<TransactionPrintModel> TransactionGrid;
	private SfGrid<AdvancePrintModel> AdvanceGrid;
	private SfGrid<AdvancePrintModel> AdvanceTakenOnGrid;
	private SfGrid<AdvancePaymentModeTotalsModel> AdvancePaymentModeGrid;

	private List<TransactionPrintModel> TransactionPrintModels { get; set; } = [];
	private List<AdvancePrintModel> AdvancePrintModels { get; set; } = [];
	private List<AdvancePrintModel> AdvanceTakenOnPrintModels { get; set; } = [];
	private List<AdvancePaymentModeTotalsModel> AdvancePaymentModeTotalsModels { get; set; } = [];

	private readonly List<LocationModel> locations = [];
	private readonly List<TransactionTotalsModel> transactionTotalsModel = [];
	private readonly List<AdvanceTotalsModel> advanceTotalsModel = [];

	protected override async Task OnInitializedAsync() => await LoadData();

	private async Task LoadData()
	{
		CurrentDateTime = DateTime.Now.AddHours(5).AddMinutes(30);
		TakenOnDate = CurrentDateTime;

		locations.Clear();
		var activeLocations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		foreach (var location in activeLocations)
			locations.Add(location);

		await LoadTransactionsAdvances();
		await LoadAdvanceByTakenOn();
	}

	private async Task LoadTransactionsAdvances()
	{
		TransactionPrintModels.Clear();
		TransactionPrintModels.AddRange(await TransactionData.LoadTransactionsByDateLocation(FromDateTime, ToDateTime, SelectedLocationId));

		AdvancePrintModels.Clear();
		AdvancePrintModels.AddRange(
		ToDateTime.TimeOfDay < TimeSpan.FromHours(17)
			? await AdvanceData.LoadAdvancesByForDateLocation(FromDateTime.Date, ToDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), SelectedLocationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(FromDateTime.Date, ToDateTime.Date, SelectedLocationId));

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

	private async Task LoadAdvanceByTakenOn()
	{
		AdvanceTakenOnPrintModels.Clear();
		AdvanceTakenOnPrintModels.AddRange(await AdvanceData.LoadAdvancesByTakenOnLocation(TakenOnDate, SelectedLocationId));

		AdvancePaymentModeTotalsModels.Clear();
		AdvancePaymentModeTotalsModels.AddRange(await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(TakenOnDate, SelectedLocationId));
	}

	private async Task OnFromValueChanged(ChangedEventArgs<DateTime> args)
	{
		FromDateTime = args.Value;
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
	}

	private async Task OnToValueChanged(ChangedEventArgs<DateTime> args)
	{
		ToDateTime = args.Value;
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
	}

	private async Task OnTakenOnValueChanged(ChangedEventArgs<DateTime> args)
	{
		TakenOnDate = args.Value;
		await LoadAdvanceByTakenOn();
		await AdvanceTakenOnGrid.Refresh();
		await AdvancePaymentModeGrid.Refresh();
	}

	private async Task OnLocationChanged()
	{
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
		await AdvanceTakenOnGrid.Refresh();
		await AdvancePaymentModeGrid.Refresh();
		StateHasChanged();
	}

	private async Task DetailedButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"DetailedReport.pdf",
			Convert.ToBase64String((await PDF.Detail(FromDateTime, ToDateTime, SelectedLocationId)).ToArray()));

	private async Task ExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"DetailedReport.xlsx",
			Convert.ToBase64String((await Excel.TransactionAdvanceExcel(FromDateTime, ToDateTime, SelectedLocationId)).ToArray()));

	private async Task AdvanceTakeOnPDFButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"AdvanceTakeOnReport.pdf",
			Convert.ToBase64String((await PDF.AdvanceTakeOn(TakenOnDate, SelectedLocationId)).ToArray()));

	private async Task AdvanceTakeOnExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"AdvanceTakeOnReport.xlsx",
			Convert.ToBase64String((await Excel.AdvanceTakeOnExcel(TakenOnDate, SelectedLocationId)).ToArray()));

	public async Task ToolbarClickHandler(ClickEventArgs args)
	{
		if (args.Item.Id == "TransactionGrid_pdfexport")
			await TransactionGrid.ExportToPdfAsync();

		if (args.Item.Id == "TransactionGrid_excelexport")
			await TransactionGrid.ExportToExcelAsync();

		if (args.Item.Id == "AdvanceGrid_pdfexport")
			await AdvanceGrid.ExportToPdfAsync();

		if (args.Item.Id == "AdvanceGrid_excelexport")
			await AdvanceGrid.ExportToExcelAsync();

		if (args.Item.Id == "AdvanceTakenOnGrid_pdfexport")
			await AdvanceTakenOnGrid.ExportToPdfAsync();

		if (args.Item.Id == "AdvanceTakenOnGrid_excelexport")
			await AdvanceTakenOnGrid.ExportToExcelAsync();
	}
}