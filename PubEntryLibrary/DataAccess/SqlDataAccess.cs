using System.Data;

using Dapper;

using Microsoft.Data.SqlClient;

namespace PubEntryLibrary.DataAccess;

static class SqlDataAccess
{
	//static string ConnectionString = "Server=AadiLapi;Integrated security=SSPI;database=PubEntry;TrustServerCertificate=True";
	static string ConnectionString = $"Server=tcp:aadiserver.database.windows.net,1433;Initial Catalog=PubEntry;Persist Security Info=False;User ID=aadisql;Password={Secrets.DatabasePassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

	public static async Task RunSQL(string sql, string databaseName)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		await connection.ExecuteAsync(sql);
	}	

	public static async Task<IEnumerable<T>> LoadDataSQL<T>(string sql, string databaseName)
	{
		using IDbConnection connection = new SqlConnection(ConnectionString);

		return await connection.QueryAsync<T>(sql);
	}
}
