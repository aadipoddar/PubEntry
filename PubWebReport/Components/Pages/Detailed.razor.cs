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

	private DateTime CurrentDateTime { get; set; } = DateTime.Now.AddHours(5).AddMinutes(30);

	private SfGrid<TransactionPrintModel> TransactionGrid;
	private SfGrid<AdvancePrintModel> AdvanceGrid;
	private List<TransactionPrintModel> TransactionPrintModels { get; set; } = [];
	private List<AdvancePrintModel> AdvancePrintModels { get; set; } = [];

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

		await LoadTransactionsAdvances();
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

	public async Task OnFromValueChanged(ChangedEventArgs<DateTime> args)
	{
		FromDateTime = args.Value;
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
	}

	public async Task OnToValueChanged(ChangedEventArgs<DateTime> args)
	{
		ToDateTime = args.Value;
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
	}

	private async Task OnLocationChanged()
	{
		await LoadTransactionsAdvances();
		await TransactionGrid.Refresh();
		await AdvanceGrid.Refresh();
		StateHasChanged();
	}

	private async Task DetailedButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"DetailedReport.pdf",
			Convert.ToBase64String((await DetailPrint.PrintDetail(FromDateTime, ToDateTime, SelectedLocationId)).ToArray()));

	private async Task ExcelButtonClicked() =>
		await JS.InvokeVoidAsync(
			"saveAsFile",
			"ExcelReport.xlsx",
			Convert.ToBase64String((await Excel.ExcelExport(FromDateTime, ToDateTime, SelectedLocationId)).ToArray()));

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
	}
}