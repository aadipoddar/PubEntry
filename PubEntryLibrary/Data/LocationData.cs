namespace PubEntryLibrary.Data;

public class LocationData
{
	public static async Task LocationInsert(LocationModel locationModel) =>
			await SqlDataAccess.SaveData("LocationInsert", locationModel);

	public static async Task LocationUpdate(LocationModel locationModel) =>
			await SqlDataAccess.SaveData("LocationUpdate", locationModel);

	public static async Task<IEnumerable<LocationModel>> LoadActiveLocations() =>
			await SqlDataAccess.LoadData<LocationModel, dynamic>("LoadActiveLocations", new { });
}