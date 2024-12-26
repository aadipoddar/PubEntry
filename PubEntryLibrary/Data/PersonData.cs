using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task<int> InsertPerson(PersonModel personModel) =>
			(await Task.Run(() => SqlDataAccess.LoadData<int, dynamic>("dbo.spPerson_Insert", personModel))).FirstOrDefault();

	public static async Task<int> UpdatePerson(PersonModel personModel) =>
			(await Task.Run(() => SqlDataAccess.LoadData<int, dynamic>("dbo.spPerson_Update", personModel))).FirstOrDefault();

	public static async Task<PersonModel> GetPersonByNumber(string number) =>
			(await Task.Run(() => SqlDataAccess.LoadData<PersonModel, dynamic>("dbo.spLoad_Person_ByNumber", new { Number = number }))).FirstOrDefault();
}
