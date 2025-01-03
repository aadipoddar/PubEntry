﻿namespace PubEntryLibrary.Models.Printing;

public class ReceiptModel
{
	public string LocationName { get; set; }
	public int ReceiptId { get; set; }
	public DateTime ReceiptDate { get; set; }
	public string PersonName { get; set; }
	public string PersonNumber { get; set; }
	public bool PersonLoyalty { get; set; }
	public string Reservation { get; set; }
	public int TotalPerson { get; set; }
	public int Male { get; set; }
	public int Female { get; set; }
	public string ApprovedBy { get; set; }
	public string EnteredBy { get; set; }
}