namespace PubEntryLibrary.Models.Printing;

public class DetailedAdvancePrintModel
{
	public int AdvanceId { get; set; }
	public string PersonName { get; set; }
	public string PersonNumber { get; set; }
	public char Loyalty { get; set; }
	public DateTime TakenOn { get; set; }
	public DateTime TakenFor { get; set; }
	public string ApprovedBy { get; set; }
	public int Booking { get; set; }
	public int AdvancePaid { get; set; }
	public string PaymentModes { get; set; }
	public string SlipId { get; set; }
	public string EntryPaid { get; set; }
	public string SlipDate { get; set; }
	public int Total { get; set; }
}