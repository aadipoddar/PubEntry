namespace PubEntryLibrary.Data;

public class TransactionData
{
	public static async Task<int> TransactionInsert(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionInsert", transactionModel)).FirstOrDefault();

	public static async Task<int> TransactionUpdate(TransactionModel transactionModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("TransactionUpdate", transactionModel)).FirstOrDefault();

	public static async Task AdvanceInsert(AdvanceModel advanceModel) =>
			await SqlDataAccess.SaveData("AdvanceInsert", advanceModel);

	public static async Task<AdvanceModel> LoadAdvanceByDateLocationPerson(int locationId, int personId) =>
			(await SqlDataAccess.LoadData<AdvanceModel, dynamic>("LoadAdvanceByDateLocationPerson", new { LocationId = locationId, PersonId = personId })).FirstOrDefault();

	public static async Task ClearAdvance(int advanceId) =>
			await SqlDataAccess.SaveData("ClearAdvance", new { Id = advanceId });
}
