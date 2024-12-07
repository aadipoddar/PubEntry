namespace PubEntryLibrary.Models;

public class PersonModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Number { get; set; }
	public int Amount { get; set; }
	public int ModeOfPayment { get; set; }
	public DateTime EntryTime { get; set; }
	public int EmployeeId { get; set; }
	public int LocationId { get; set; }
}
