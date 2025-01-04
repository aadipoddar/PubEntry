﻿using System.Data;

using Dapper;

using Microsoft.Data.SqlClient;

namespace PubEntryLibrary.DataAccess;

static class SqlDataAccess
{
	static string ConnectionString = $"Server=tcp:goldenpark.database.windows.net,1433;Initial Catalog={Secrets.DatabaseName};Persist Security Info=False;User ID=aadisql;Password={Secrets.DatabasePassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

	public static async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		List<T> rows = (await connection.QueryAsync<T>(storedProcedure, parameters,
			commandType: CommandType.StoredProcedure)).ToList();

		return rows;
	}

	public static async Task SaveData<T>(string storedProcedure, T parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
