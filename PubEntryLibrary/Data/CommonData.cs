﻿namespace PubEntryLibrary.Data;

public static class CommonData
{
	public static async Task<List<T>> LoadTableData<T>(string TableName) where T : new() =>
			await SqlDataAccess.LoadData<T, dynamic>(StoredProcedure.LoadTableData, new { TableName });

	public static async Task<T> LoadTableDataById<T>(string TableName, int Id) where T : new() =>
			(await SqlDataAccess.LoadData<T, dynamic>(StoredProcedure.LoadTableDataById, new { TableName, Id })).FirstOrDefault();

	public static async Task<List<T>> LoadTableDataByStatus<T>(string TableName, bool Status = true) where T : new() =>
			await SqlDataAccess.LoadData<T, dynamic>(StoredProcedure.LoadTableDataByStatus, new { TableName, Status });
}
