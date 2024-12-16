namespace PubEntryLibrary.Models;

public class EmployeeModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public int LocationId { get; set; }
	public int ActiveStatus { get; set; }
}
