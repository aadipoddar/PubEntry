using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task InsertPersonTableData(string personName, string personNumber) =>
			await SqlDataAccess.RunSQL($"INSERT INTO PersonTable VALUES ('{personName}', {personNumber})", "PubEntry");

	public static async Task<List<PersonModel>> GetPersonByNumber(string number) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Number = '{number}'", "PubEntry") as List<PersonModel>;

	public static async Task<List<PersonModel>> GetPersonByName(string name) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Name = '{name}'", "PubEntry") as List<PersonModel>;

	public static async Task<List<PersonModel>> GetPersonById(string name) =>
		await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Name = '{name}'", "PubEntry") as List<PersonModel>;
}
