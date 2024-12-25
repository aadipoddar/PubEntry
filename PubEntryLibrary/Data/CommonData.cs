using PubEntryLibrary.DataAccess;

namespace PubEntryLibrary.Data;

public static class CommonData
{
	public static async Task<IEnumerable<T>> LoadTableData<T>(string tableName) where T : new() =>
			await Task.Run(() => SqlDataAccess.LoadData<T, dynamic>("dbo.spLoad_TableData", new { TableName = tableName }));

	public static async Task<IEnumerable<T>> LoadTableDataById<T>(string tableName, int id) where T : new() =>
			await Task.Run(() => SqlDataAccess.LoadData<T, dynamic>("dbo.spLoad_TableData_ById", new { TableName = tableName, Id = id }));
}
