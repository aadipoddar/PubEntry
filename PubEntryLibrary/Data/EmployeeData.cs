using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class EmployeeData
{
	public static async Task InsertEmployeeTableData(EmployeeModel employeeModel) =>
		await SqlDataAccess.RunSQL($"INSERT INTO EmployeeTable VALUES ('{employeeModel.Name}', '{employeeModel.Password}', {employeeModel.LocationId})", "PubEntry");

	public static async Task<List<EmployeeModel>> LoadEmployeeByLocation(int locationId) =>
		await SqlDataAccess.LoadDataSQL<EmployeeModel>($"SELECT * FROM EmployeeTable WHERE LocationId = {locationId}", "PubEntry") as List<EmployeeModel>;

	public static async Task<string> GetEmployeePasswordById(int employeeId) =>
		(await CommonData.GetById<EmployeeModel>("EmployeeTable", employeeId) as List<EmployeeModel>).FirstOrDefault().Password;
}
