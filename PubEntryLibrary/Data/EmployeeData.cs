using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class EmployeeData
{
	public static async Task InsertEmployeeTableData(EmployeeModel employeeModel) =>
		await SqlDataAccess.RunSQL($"INSERT INTO EmployeeTable VALUES ('{employeeModel.Name}', '{employeeModel.Password}', {employeeModel.LocationId}, {employeeModel.ActiveStatus})", Secrets.DatabaseName);

	public static async Task UpdateEmployeeTableData(EmployeeModel employeeModel, int id) =>
		await SqlDataAccess.RunSQL($"UPDATE EmployeeTable SET Name = '{employeeModel.Name}', Password = '{employeeModel.Password}', LocationId = {employeeModel.LocationId}, ActiveStatus = {employeeModel.ActiveStatus} WHERE Id = {id}", Secrets.DatabaseName);

	public static async Task<List<EmployeeModel>> LoadActiveEmployeeByLocation(int locationId) =>
		await SqlDataAccess.LoadDataSQL<EmployeeModel>($"SELECT * FROM EmployeeTable WHERE LocationId = {locationId} AND ActiveStatus = 0", Secrets.DatabaseName) as List<EmployeeModel>;

	public static async Task<string> GetEmployeePasswordById(int employeeId) =>
		(await CommonData.GetById<EmployeeModel>("EmployeeTable", employeeId) as List<EmployeeModel>).FirstOrDefault().Password;
}
