using PubEntryLibrary.DataAccess;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Data;

public class EmployeeData
{
	public static async Task InsertEmployee(EmployeeModel employeeModel) =>
			await Task.Run(() => SqlDataAccess.SaveData("dbo.spEmployee_Insert", employeeModel));

	public static async Task UpdateEmployee(EmployeeModel employeeModel) =>
			await Task.Run(() => SqlDataAccess.SaveData("dbo.spEmployee_Update", employeeModel));

	public static async Task<IEnumerable<EmployeeModel>> LoadActiveEmployeeByLocation(int locationId) =>
			await Task.Run(() => SqlDataAccess.LoadData<EmployeeModel, dynamic>("dbo.spLoad_ActiveEmployee_ByLocationId", new { LocationId = locationId }));
}
