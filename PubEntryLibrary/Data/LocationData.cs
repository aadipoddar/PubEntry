namespace PubEntryLibrary.Data;

public class LocationData
{
	public static async Task InsertLocation(LocationModel locationModel) =>
			await SqlDataAccess.SaveData(StoredProcedureNames.InsertLocation, locationModel);
}