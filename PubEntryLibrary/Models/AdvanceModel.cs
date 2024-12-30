namespace PubEntryLibrary.Models;

public class AdvanceModel
{
	public int Id { get; set; }
	public int LocationId { get; set; }
	public int PersonId { get; set; }
	public int Amount { get; set; }
	public DateTime AdvanceDate { get; set; }
	public bool Cleared { get; set; }
}