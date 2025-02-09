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
	public int PaymentModeId { get; set; }
	public int Amount { get; set; }
}

public class AdvancePrintModel
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

public class AdvancePaymentModeTotalsModel
{
	public string PaymentMode { get; set; }
	public int Amount { get; set; }
}

public class AdvanceTotalsModel
{
	public int LocationId { get; set; }
	public int TotalBooking { get; set; }
	public int RedeemedBooking { get; set; }
	public int NotRedeemedBooking { get; set; }
	public int TotalAdvance { get; set; }
	public int RedeemedAdvance { get; set; }
	public int NotRedeemedAdvance { get; set; }
}