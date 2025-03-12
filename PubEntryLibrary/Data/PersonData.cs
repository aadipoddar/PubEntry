namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task<int> InsertPerson(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedureNames.InsertPerson, personModel)).FirstOrDefault();

	public static async Task<int> UpdatePerson(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedureNames.UpdatePerson, personModel)).FirstOrDefault();

	public static async Task<PersonModel> LoadPersonByNumber(string Number) =>
			(await SqlDataAccess.LoadData<PersonModel, dynamic>(StoredProcedureNames.LoadPersonByNumber, new { Number })).FirstOrDefault();

	public static async Task<List<PersonModel>> LoadPersonByName(string Name) =>
			await SqlDataAccess.LoadData<PersonModel, dynamic>(StoredProcedureNames.LoadPersonByName, new { Name });
}
