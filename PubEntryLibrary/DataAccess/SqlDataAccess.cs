using System.Data;

using Dapper;

using Microsoft.Data.SqlClient;

namespace PubEntryLibrary.DataAccess;

static class SqlDataAccess
{
	static string ConnectionString = $"Server=tcp:aadiserver.database.windows.net,1433;Initial Catalog=PubEntry;Persist Security Info=False;User ID=aadisql;Password={Secrets.DatabasePassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

	public static async Task RunSQL(string sql)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		await connection.ExecuteAsync(sql);
	}

	public static async Task<IEnumerable<T>> LoadDataSQL<T>(string sql)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		return await connection.QueryAsync<T>(sql);
	}

	public static List<T> LoadData<T, U>(string storedProcedure, U parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		List<T> rows = connection.Query<T>(storedProcedure, parameters,
			commandType: CommandType.StoredProcedure).ToList();

		return rows;
	}

	public static void SaveData<T>(string storedProcedure, T parameters)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
