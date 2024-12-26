using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models.Printing;

namespace PubEntryLibrary.Data;

public class PrintData
{
	public static async Task<IEnumerable<ReceiptModel>> LoadReceiptDetails(int transactionId) =>
			await Task.Run(() => SqlDataAccess.LoadData<ReceiptModel, dynamic>("dbo.spReceipt_Details", new { TransactionId = transactionId }));

	public static async Task<IEnumerable<DetailedPrintModel>> LoadDetailedPrint(string fromDateTime, string toDateTime, int locationId) =>
			await Task.Run(() => SqlDataAccess.LoadData<DetailedPrintModel, dynamic>("dbo.sp_GetTransactions_ByDate_AndLocation", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId }));

	public static async Task<IEnumerable<TransactionTotalsModel>> LoadTransactionTotals(string fromDateTime, string toDateTime, int locationId) =>
			await Task.Run(() => SqlDataAccess.LoadData<TransactionTotalsModel, dynamic>("dbo.sp_GetTransaction_Totals", new { FromDate = fromDateTime, ToDate = toDateTime, LocationId = locationId }));
}