namespace PubEntryLibrary.Data;

public class ReservationTypeData
{
	public static async Task InsertReservationType(ReservationTypeModel reservationTypeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.InsertReservationType, reservationTypeModel);

	public static async Task UpdateReservationType(ReservationTypeModel reservationTypeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateReservationType, reservationTypeModel);
}