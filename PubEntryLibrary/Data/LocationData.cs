using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class LocationData
{
	public static async Task InsertLocationTableData(LocationModel locationModel) =>
		await SqlDataAccess.RunSQL($"INSERT INTO LocationTable VALUES ('{locationModel.Name}', {locationModel.ActiveStatus})", Secrets.DatabaseName);

	public static async Task UpdateLocationTableData(LocationModel locationModel, int id) =>
		await SqlDataAccess.RunSQL($"UPDATE LocationTable SET Name = '{locationModel.Name}', ActiveStatus = {locationModel.ActiveStatus} WHERE Id = {id}", Secrets.DatabaseName);

	public static async Task<List<LocationModel>> LoadActiveLocations() =>
		await SqlDataAccess.LoadDataSQL<LocationModel>($"SELECT * FROM LocationTable WHERE ActiveStatus = 0", Secrets.DatabaseName) as List<LocationModel>;
}