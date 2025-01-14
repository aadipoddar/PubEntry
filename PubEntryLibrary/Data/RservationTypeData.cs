﻿namespace PubEntryLibrary.Data;

public class ReservationTypeData
{
	public static async Task InsertReservationType(ReservationTypeModel reservationTypeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.InsertReservationType, reservationTypeModel);

	public static async Task Update_ReservationType(ReservationTypeModel reservationTypeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateReservationType, reservationTypeModel);
}