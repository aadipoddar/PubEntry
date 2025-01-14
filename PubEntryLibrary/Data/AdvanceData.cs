namespace PubEntryLibrary.Data;

public static class AdvanceData
{
	public static async Task<int> InsertAdvance(AdvanceModel advanceModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.InsertAdvance, advanceModel)).FirstOrDefault();

	public static async Task UpdateAdvance(AdvanceModel advanceModel) =>
			await SqlDataAccess.SaveData<dynamic>(StoredProcedure.UpdateAdvance, advanceModel);

	public static async Task<int> InsertAdvanceDetail(AdvanceDetailModel advanceDetailModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.InsertAdvanceDetail, advanceDetailModel)).FirstOrDefault();

	public static async Task DeleteAdvanceDetails(int AdvanceId) =>
			await SqlDataAccess.SaveData<dynamic>(StoredProcedure.DeleteAdvanceDetails, new { AdvanceId });

	public static async Task ClearAdvance(int AdvanceId, int TransactionId) =>
			await SqlDataAccess.SaveData<dynamic>(StoredProcedure.ClearAdvance, new { AdvanceId, TransactionId });

	public static async Task<AdvanceModel> LoadAdvanceByDateLocationPerson(int LocationId, int PersonId, DateTime? AdvanceDate = null) =>
			(await SqlDataAccess.LoadData<AdvanceModel, dynamic>(StoredProcedure.LoadAdvanceByDateLocationPerson,
				new { LocationId, PersonId, AdvanceDate })).FirstOrDefault();

	public static async Task<List<AdvanceDetailModel>> LoadAdvanceDetailByAdvanceId(int AdvanceId) =>
		await SqlDataAccess.LoadData<AdvanceDetailModel, dynamic>(StoredProcedure.LoadAdvanceDetailByAdvanceId, new { AdvanceId });

	public static async Task<AdvanceModel> LoadAdvanceByTransactionId(int TransactionId) =>
		(await SqlDataAccess.LoadData<AdvanceModel, dynamic>(StoredProcedure.LoadAdvanceByTransactionId, new { TransactionId })).FirstOrDefault();

	public static async Task<IEnumerable<AdvancePrintModel>> LoadAdvancesByForDateLocation(DateTime FromDate, DateTime ToDate, int LocationId) =>
			await SqlDataAccess.LoadData<AdvancePrintModel, dynamic>(StoredProcedure.LoadAdvancesByForDateLocation, new { FromDate, ToDate, LocationId });

	public static async Task<IEnumerable<AdvancePrintModel>> LoadAdvancesByTakenOnLocation(DateTime TakenOn, int LocationId) =>
			await SqlDataAccess.LoadData<AdvancePrintModel, dynamic>(StoredProcedure.LoadAdvancesByTakenOnLocation, new { TakenOn, LocationId });

	public static async Task<List<AdvancePaymentModeTotalsModel>> LoadAdvancePaymentModeTotalsByTakenOn(DateTime TakenOn, int LocationId) =>
		await SqlDataAccess.LoadData<AdvancePaymentModeTotalsModel, dynamic>(StoredProcedure.LoadAdvancePaymentModeTotalsByTakenOn, new { TakenOn, LocationId });

	public static async Task<AdvanceTotalsModel> LoadAdvanceTotalsByForDateLocation(DateTime FromDate, DateTime ToDate, int LocationId) =>
		(await SqlDataAccess.LoadData<AdvanceTotalsModel, dynamic>(StoredProcedure.LoadAdvanceTotalsByForDateLocation, new { FromDate, ToDate, LocationId })).FirstOrDefault();
}