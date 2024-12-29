using PubEntryLibrary.Models.Printing;

namespace PubEntryLibrary.Data;

public class PrintData
{
	public static async Task<IEnumerable<ReceiptModel>> LoadReceiptDetails(int transactionId) =>
			await SqlDataAccess.LoadData<ReceiptModel, dynamic>("LoadReceiptDetails", new { TransactionId = transactionId });

	public static async Task<IEnumerable<DetailedPrintModel>> LoadTransactionsByDateAndLocation(string fromDateTime, string toDateTime, int locationId) =>
			await SqlDataAccess.LoadData<DetailedPrintModel, dynamic>("LoadTransactionsByDateAndLocation", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId });

	public static async Task<IEnumerable<TransactionTotalsModel>> LoadTransactionTotals(string fromDateTime, string toDateTime, int locationId) =>
			await SqlDataAccess.LoadData<TransactionTotalsModel, dynamic>("LoadTransactionTotals", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId });
}