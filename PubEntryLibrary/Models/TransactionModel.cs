namespace PubEntryLibrary.Models;

public class TransactionModel
{
	public int Id { get; set; }
	public int PersonId { get; set; }
	public int Male { get; set; }
	public int Female { get; set; }
	public int Cash { get; set; }
	public int Card { get; set; }
	public int UPI { get; set; }
	public int Amex { get; set; }
	public int ReservationTypeId { get; set; }
	public DateTime DateTime { get; set; }
	public string ApprovedBy { get; set; }
	public int LocationId { get; set; }
	public int UserId { get; set; }
}

public class TransactionPrintModel
{
	public int Id { get; set; }
	public int LocationId { get; set; }
	public string LocationName { get; set; }
	public string PersonName { get; set; }
	public string PersonNumber { get; set; }
	public char Loyalty { get; set; }
	public string Reservation { get; set; }
	public int Male { get; set; }
	public int Female { get; set; }
	public int Cash { get; set; }
	public int Card { get; set; }
	public int UPI { get; set; }
	public int Amex { get; set; }
	public string ApprovedBy { get; set; }
	public string EnteredBy { get; set; }
	public DateTime DateTime { get; set; }
}

public class TransactionTotalsModel
{
	public int Male { get; set; }
	public int Female { get; set; }
	public int Loyalty { get; set; }
	public int Cash { get; set; }
	public int Card { get; set; }
	public int UPI { get; set; }
	public int Amex { get; set; }
}