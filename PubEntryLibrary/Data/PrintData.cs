using PubEntryLibrary.Models.Printing;

namespace PubEntryLibrary.Data;

public class PrintData
{
	public static async Task<IEnumerable<ReceiptModel>> LoadReceiptDetails(int transactionId) =>
			await SqlDataAccess.LoadData<ReceiptModel, dynamic>("LoadReceiptDetails", new { TransactionId = transactionId });

	public static async Task<IEnumerable<DetailedTransactionPrintModel>> LoadTransactionsByDateAndLocation(DateTime fromDateTime, DateTime toDateTime, int locationId) =>
			await SqlDataAccess.LoadData<DetailedTransactionPrintModel, dynamic>("LoadTransactionsByDateAndLocation", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId });

	public static async Task<IEnumerable<TransactionTotalsModel>> LoadTransactionTotals(DateTime fromDateTime, DateTime toDateTime, int locationId) =>
			await SqlDataAccess.LoadData<TransactionTotalsModel, dynamic>("LoadTransactionTotals", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId });

	public static async Task<IEnumerable<DetailedAdvancePrintModel>> LoadAdvancesByDateAndLocation(DateTime fromDateTime, DateTime toDateTime, int locationId) =>
			await SqlDataAccess.LoadData<DetailedAdvancePrintModel, dynamic>("LoadAdvancesByDateAndLocation", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId });
}