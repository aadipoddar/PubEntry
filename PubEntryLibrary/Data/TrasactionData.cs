using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> InsertTransactionTableData(TransactionModel transaction) =>
			(int)Convert.ToInt64((await SqlDataAccess.LoadDataSQL<TransactionModel>($"INSERT INTO TransactionTable (PersonId, Male, Female, Cash, Card, UPI, Amex, ReservationType, ApprovedBy, LocationId, EmployeeId) Output Inserted.Id VALUES ({transaction.PersonId}, {transaction.Male}, {transaction.Female}, {transaction.Cash}, {transaction.Card}, {transaction.UPI}, {transaction.Amex}, {transaction.ReservationType}, '{transaction.ApprovedBy}', {transaction.LocationId}, {transaction.EmployeeId})")).FirstOrDefault().Id);

	public static async Task<List<TransactionModel>> GetTransactionsByDateRangeAndLocation(string fromDateTime, string toDateTime, int locationId) =>
		await SqlDataAccess.LoadDataSQL<TransactionModel>($"SELECT * FROM TransactionTable WHERE DateTime >= '{fromDateTime}' AND DateTime <= '{toDateTime}' AND LocationId = {locationId}") as List<TransactionModel>;
}
