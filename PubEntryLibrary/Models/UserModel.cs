namespace PubEntryLibrary.Models;

public class UserModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public int LocationId { get; set; }
	public bool Admin { get; set; }
	public bool Status { get; set; }
}

public class UserLocationModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Password { get; set; }
	public int LocationId { get; set; }
	public string LocationName { get; set; }
	public bool Admin { get; set; }
	public bool Status { get; set; }
}