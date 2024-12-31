namespace PubEntryLibrary.Models;

public class TransactionModel
{
	public int Id { get; set; }
	public int LocationId { get; set; }
	public int UserId { get; set; }
	public DateTime DateTime { get; set; }
	public int PersonId { get; set; }
	public int Male { get; set; }
	public int Female { get; set; }
	public int ReservationType { get; set; }
	public string ApprovedBy { get; set; }
}
