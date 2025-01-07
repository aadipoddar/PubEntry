namespace PubEntryLibrary.Models.Printing;

public class DetailedAdvancePrintModel
{
	public int Adv_Id { get; set; }
	public string Name { get; set; }
	public string Number { get; set; }
	public char Loyalty { get; set; }
	public DateTime Adv_Pymt_DT { get; set; }
	public DateTime Adv_For_DT { get; set; }
	public string Remarks { get; set; }
	public int Booking_Amt { get; set; }
	public int Adv_Paid { get; set; }
	public string Pay_Mode { get; set; }
	public string Slip_No { get; set; }
	public string Entry_Paid { get; set; }
	public string Slip_DT { get; set; }
	public int Total_Amt { get; set; }
}