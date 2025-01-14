﻿namespace PubEntryLibrary.Data;

public class LocationData
{
	public static async Task InsertLocation(LocationModel locationModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.InsertLocation, locationModel);

	public static async Task Update_Location(LocationModel locationModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateLocation, locationModel);
}