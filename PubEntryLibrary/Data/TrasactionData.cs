namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> TransactionInsert(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionInsert", transactionModel)).FirstOrDefault();

	public static async Task<int> TransactionUpdate(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionUpdate", transactionModel)).FirstOrDefault();
}
