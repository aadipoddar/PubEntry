namespace PubEntryLibrary.Data;

public class AdvanceData
{
	public static async Task<int> AdvanceInsert(AdvanceModel advanceModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("AdvanceInsert", advanceModel)).FirstOrDefault();

	public static async Task AdvanceUpdate(AdvanceModel advanceModel) =>
			await SqlDataAccess.SaveData<dynamic>("AdvanceUpdate", advanceModel);

	public static async Task<int> AdvanceDetailInsert(AdvanceDetailModel advanceDetailModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("AdvanceDetailInsert", advanceDetailModel)).FirstOrDefault();

	public static async Task<AdvanceModel> LoadAdvanceByDateLocationPerson(int locationId, int personId, DateTime? advanceDate = null) =>
			(await SqlDataAccess.LoadData<AdvanceModel, dynamic>("LoadAdvanceByDateLocationPerson",
				new { LocationId = locationId, PersonId = personId, AdvanceDate = advanceDate })).FirstOrDefault();

	public static async Task<List<AdvanceDetailModel>> LoadAdvanceDetailByAdvanceId(int advanceId) =>
		await SqlDataAccess.LoadData<AdvanceDetailModel, dynamic>("LoadAdvanceDetailByAdvanceId", new { AdvanceId = advanceId });

	public static async Task AdvanceDetailDeleteByAdvanceId(int advanceId) =>
		await SqlDataAccess.SaveData<dynamic>("AdvanceDetailDeleteByAdvanceId", new { AdvanceId = advanceId });

	public static async Task AdvanceClear(int advanceId, int transactionId) =>
		await SqlDataAccess.SaveData<dynamic>("AdvanceClear", new { Id = advanceId, TransactionId = transactionId });
}