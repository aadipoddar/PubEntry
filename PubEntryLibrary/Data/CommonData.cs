namespace PubEntryLibrary.Data;

public static class CommonData
{
	public static async Task<IEnumerable<T>> LoadTableData<T>(string tableName) where T : new() =>
			await SqlDataAccess.LoadData<T, dynamic>("LoadTableData", new { TableName = tableName });

	public static async Task<IEnumerable<T>> LoadTableDataById<T>(string tableName, int id) where T : new() =>
			await SqlDataAccess.LoadData<T, dynamic>("LoadTableDataById", new { TableName = tableName, Id = id });
}
