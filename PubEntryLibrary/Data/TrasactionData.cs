using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task InsertTransactionTableData(TransactionModel transaction) =>
			await SqlDataAccess.RunSQL($"INSERT INTO TransactionTable VALUES ({transaction.PersonId}, {transaction.Male}, {transaction.Female}, {transaction.Cash}, {transaction.Card}, {transaction.UPI}, {transaction.Amex}, {transaction.ReservationType}, '{transaction.DateTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{transaction.ApprovedBy}', {transaction.LocationId}, {transaction.EmployeeId})", "PubEntry");

	public static async Task<int> GetTransactionIdbyDateAndPersonId(string dateTime, int personId) =>
		(int)Convert.ToInt64((await SqlDataAccess.LoadDataSQL<TransactionModel>($"SELECT Id FROM TransactionTable WHERE DateTime = '{dateTime}' AND PersonId = {personId}", "PubEntry")).FirstOrDefault().Id);

	public static async Task<List<TransactionModel>> GetTransactionsByDateRangeAndLocation(string fromDateTime, string toDateTime, int locationId) =>
		await SqlDataAccess.LoadDataSQL<TransactionModel>($"SELECT * FROM TransactionTable WHERE DateTime >= '{fromDateTime}' AND DateTime <= '{toDateTime}' AND LocationId = {locationId}", "PubEntry") as List<TransactionModel>;
}
