using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task InsertPersonTableData(string personName, string personNumber, int loyalty) =>
			await SqlDataAccess.RunSQL($"INSERT INTO PersonTable VALUES ('{personName}', {personNumber}, {loyalty})");

	public static async Task UpdatePersonTableData(string personNumber, int loyalty) =>
		await SqlDataAccess.RunSQL($"UPDATE PersonTable SET Loyalty = {loyalty} WHERE Number = '{personNumber}'");

	public static async Task<List<PersonModel>> GetPersonByNumber(string number) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Number = '{number}'") as List<PersonModel>;

	public static async Task<List<PersonModel>> GetPersonByName(string name) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Name = '{name}'") as List<PersonModel>;

	public static async Task<List<PersonModel>> GetPersonById(string name) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Name = '{name}'") as List<PersonModel>;
}
