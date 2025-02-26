﻿using System.Data;

using Dapper;

using Microsoft.Data.SqlClient;

namespace PubEntryLibrary.DataAccess;

static class SqlDataAccess
{
	public static async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionStrings.Azure);

		List<T> rows = [.. (await connection.QueryAsync<T>(storedProcedure, parameters,
			commandType: CommandType.StoredProcedure))];

		return rows;
	}

	public static async Task SaveData<T>(string storedProcedure, T parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionStrings.Azure);

		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}