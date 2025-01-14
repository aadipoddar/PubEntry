namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> InsertTransaction(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.InsertTransaction, transactionModel)).FirstOrDefault();

	public static async Task<int> UpdateTransaction(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.UpdateTransaction, transactionModel)).FirstOrDefault();

	public static async Task<IEnumerable<TransactionPrintModel>> LoadTransactionsByDateLocation(DateTime FromDate, DateTime ToDate, int LocationId) =>
			await SqlDataAccess.LoadData<TransactionPrintModel, dynamic>(StoredProcedure.LoadTransactionsByDateLocation, new { FromDate, ToDate, LocationId });

	public static async Task<TransactionTotalsModel> LoadTransactionTotalsByDateLocation(DateTime FromDate, DateTime ToDate, int LocationId) =>
			(await SqlDataAccess.LoadData<TransactionTotalsModel, dynamic>(StoredProcedure.LoadTransactionTotalsByDateLocation, new { FromDate, ToDate, LocationId })).FirstOrDefault();
}
