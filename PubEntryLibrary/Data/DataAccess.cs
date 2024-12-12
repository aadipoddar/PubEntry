using PubEntryLibrary.DataAccess;

namespace PubEntryLibrary.Data;

public static class CommonData
{
	public static async Task<IEnumerable<T>> LoadTableData<T>(string tableName) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName}", "PubEntry");

	public static async Task<IEnumerable<T>> GetById<T>(string tableName, int id) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName} WHERE Id = {id}", "PubEntry");
}
