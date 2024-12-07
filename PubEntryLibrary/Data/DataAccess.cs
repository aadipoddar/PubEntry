using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public static class DataAccess
{
	public static async Task<IEnumerable<T>> LoadTableData<T>(string tableName) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName}", "PubEntry");

	public static async Task InsertPersonTableData(PersonModel person) =>
			await SqlDataAccess.RunSQL($"INSERT INTO PersonTable VALUES ({person.Id}, '{person.Name}', {person.Number}, {person.Amount}, {person.ModeOfPayment}, '{person.EntryTime}', {person.EmployeeId}, {person.LocationId})", "PubEntry");

	public static async Task<IEnumerable<T>> GetById<T>(string tableName, int id) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM {tableName} WHERE Id = {id}", "PubEntry");

	public static async Task<IEnumerable<T>> GetPeopleByLocation<T>(int locationId) where T : new() =>
			await SqlDataAccess.LoadDataSQL<T>($"SELECT * FROM PersonTable WHERE LocationId = {locationId}", "PubEntry");

	public static async Task<string> GetModOfPaymentName(int modeOfPayment) =>
		(await GetById<PaymentMethodModel>("PaymentMethodTable", modeOfPayment) as List<PaymentMethodModel>).FirstOrDefault().Name;

	public static async Task<string> GetEmployeeNameById(int employeeId) =>
		(await GetById<EmployeeModel>("EmployeeTable", employeeId) as List<EmployeeModel>).FirstOrDefault().Name;

	public static async Task<string> GetLocationNameById(int locationId) =>
		(await GetById<LocationModel>("LocationTable", locationId) as List<LocationModel>).FirstOrDefault().Name;

	public static async Task<List<PersonModel>> GetPeopleByLocation(int locationId) =>
		await DataAccess.GetPeopleByLocation<PersonModel>(locationId) as List<PersonModel>;

	public static async Task<List<PersonModel>> GetPersonByNumber(string number) =>
			await SqlDataAccess.LoadDataSQL<PersonModel>($"SELECT * FROM PersonTable WHERE Number = '{number}'", "PubEntry") as List<PersonModel>;

	public static async Task<string> GetEmployeePasswordById(int employeeId) =>
		(await GetById<EmployeeModel>("EmployeeTable", employeeId) as List<EmployeeModel>).FirstOrDefault().Password;
}
