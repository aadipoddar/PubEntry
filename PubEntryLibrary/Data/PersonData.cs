﻿namespace PubEntryLibrary.Data;

public class PersonData
{
	public static async Task<int> InsertPerson(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.InsertPerson, personModel)).FirstOrDefault();

	public static async Task<int> UpdatePerson(PersonModel personModel) =>
			(await SqlDataAccess.LoadData<int, dynamic>(StoredProcedure.UpdatePerson, personModel)).FirstOrDefault();

	public static async Task<PersonModel> LoadPersonByNumber(string Number) =>
			(await SqlDataAccess.LoadData<PersonModel, dynamic>(StoredProcedure.LoadPersonByNumber, new { Number })).FirstOrDefault();
}
