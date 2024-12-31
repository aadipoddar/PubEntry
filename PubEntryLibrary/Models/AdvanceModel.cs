namespace PubEntryLibrary.Models;

public class AdvanceModel
{
	public int Id { get; set; }
	public int TransactionId { get; set; }
	public int LocationId { get; set; }
	public int PersonId { get; set; }
	public DateTime DateTime { get; set; }
	public DateTime AdvanceDate { get; set; }
	public int BookingAmount { get; set; }
	public bool Cleared { get; set; }
}