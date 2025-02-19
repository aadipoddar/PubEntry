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
	public int UserId { get; set; }
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
	public int Id { get; set; }
	public string Name { get; set; }
	public string Number { get; set; }
	public char Loyalty { get; set; }
	public DateTime PaymentDT { get; set; }
	public DateTime ForDT { get; set; }
	public string Remarks { get; set; }
	public string User { get; set; }
	public int Booking { get; set; }
	public int Amount { get; set; }
	public string Mode { get; set; }
	public string SlipId { get; set; }
	public string Entry { get; set; }
	public string SlipDT { get; set; }
	public int Total { get; set; }
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

public class AdvancePaymentModel
{
	public int PaymentModeId { get; set; }
	public string PaymentModeName { get; set; }
	public int Amount { get; set; }
}