namespace PubEntryLibrary.Models;

public class AdvanceModel
{
	public int Id { get; set; }
	public int LocationId { get; set; }
	public int PersonId { get; set; }
	public DateTime DateTime { get; set; }
	public DateTime AdvanceDate { get; set; }
	public int Booking { get; set; }
	public string ApprovedBy { get; set; }
	public int TransactionId { get; set; }
}

public class AdvanceDetailModel
{
	public int Id { get; set; }
	public int AdvanceId { get; set; }
	public int PaymentMode { get; set; }
	public int Amount { get; set; }
}