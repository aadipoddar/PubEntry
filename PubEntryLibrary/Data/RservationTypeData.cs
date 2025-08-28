namespace PubEntryLibrary.Data;

public class ReservationTypeData
{
	public static async Task InsertReservationType(ReservationTypeModel reservationTypeModel) =>
			await SqlDataAccess.SaveData(StoredProcedureNames.InsertReservationType, reservationTypeModel);
}