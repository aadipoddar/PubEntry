namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> TransactionInsert(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionInsert", transactionModel)).FirstOrDefault();

	public static async Task<int> TransactionUpdate(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionUpdate", transactionModel)).FirstOrDefault();

	public static async Task<int> AdvanceInsert(AdvanceModel advanceModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("AdvanceInsert", advanceModel)).FirstOrDefault();

	public static async Task<AdvanceModel> LoadAdvanceByDateLocationPerson(int locationId, int personId) =>
			(await SqlDataAccess.LoadData<AdvanceModel, dynamic>("LoadAdvanceByDateLocationPerson", new { LocationId = locationId, PersonId = personId })).FirstOrDefault();

	public static async Task<int> GetTotalAdvanceAmountById(int advanceId) =>
			(await SqlDataAccess.LoadData<int, dynamic>("GetTotalAdvanceAmountById", new { AdvanceId = advanceId })).FirstOrDefault();

	public static async Task ClearAdvance(int advanceId, int transactionId) =>
			await SqlDataAccess.SaveData("ClearAdvance", new { Id = advanceId, TransactionId = transactionId });

	public static async Task<AdvanceModel> GetAdvanceByTransactionId(int transactionId) =>
			(await SqlDataAccess.LoadData<AdvanceModel, dynamic>("GetAdvanceByTransactionId", new { TransactionId = transactionId })).FirstOrDefault();

	public static async Task<List<PaymentModel>> LoadPaymentsByTransactionId(int transactionId) =>
			(await SqlDataAccess.LoadData<List<PaymentModel>, dynamic>("LoadPaymentsByTransactionId", new { TransactionId = transactionId })).FirstOrDefault();
}
