namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task<int> PersonInsert(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("PersonInsert", personModel)).FirstOrDefault();

	public static async Task<int> PersonUpdate(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>("PersonUpdate", personModel)).FirstOrDefault();

	public static async Task<PersonModel> LoadPersonByNumber(string number) =>
			(await SqlDataAccess.LoadData<PersonModel, dynamic>("LoadPersonByNumber", new { Number = number })).FirstOrDefault();
}
