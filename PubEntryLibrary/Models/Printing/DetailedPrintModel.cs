namespace PubEntryLibrary.Models.Printing;

public class DetailedPrintModel
{
	public int SlipId { get; set; }
	public string PersonName { get; set; }
	public string PersonNumber { get; set; }
	public char Loyalty { get; set; }
	public int Male { get; set; }
	public int Female { get; set; }
	public int Cash { get; set; }
	public int Card { get; set; }
	public int UPI { get; set; }
	public int Amex { get; set; }
	public string ApprovedBy { get; set; }
	public string EnteredBy { get; set; }
	public DateTime ReceiptDate { get; set; }
}