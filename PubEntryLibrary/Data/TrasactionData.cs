using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> InsertTransaction(TransactionModel transactionModel) =>
			(await Task.Run(() => SqlDataAccess.LoadData<int, dynamic>("dbo.spTransaction_Insert", transactionModel))).FirstOrDefault();

	public static async Task<int> UpdateTransaction(TransactionModel transactionModel) =>
			(await Task.Run(() => SqlDataAccess.LoadData<int, dynamic>("dbo.spTransaction_Update", transactionModel))).FirstOrDefault();
}
