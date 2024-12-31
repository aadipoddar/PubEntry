namespace PubEntryLibrary.Models;

public class PaymentModel
{
	public int Id { get; set; }
	public int TransactionId { get; set; }
	public int AdvanceId { get; set; }
	public int PaymentModeId { get; set; }
	public int Amount { get; set; }
}

public class ReceiptPaymentModel
{
	public string PaymentMode { get; set; }
	public int Amount { get; set; }
}