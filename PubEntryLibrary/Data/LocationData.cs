using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class LocationData
{
	public static async Task InsertLocation(LocationModel locationModel) =>
			await Task.Run(() => SqlDataAccess.SaveData("dbo.spLocation_Insert", locationModel));

	public static async Task UpdateLocation(LocationModel locationModel) =>
			await Task.Run(() => SqlDataAccess.SaveData("dbo.spLocation_Update", locationModel));

	public static async Task<IEnumerable<LocationModel>> LoadActiveLocations() =>
			await Task.Run(() => SqlDataAccess.LoadData<LocationModel, dynamic>("dbo.spLoad_ActiveLocation", new { }));
}