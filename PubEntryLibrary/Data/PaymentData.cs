namespace PubEntryLibrary.Data;

public class PaymentData
{
	public static async Task PaymentInsert(PaymentModel paymentModel) =>
			await SqlDataAccess.SaveData("PaymentInsert", paymentModel);

	public static async Task<List<ReceiptPaymentModel>> LoadPaymentsById(int transactionId = 0, int advanceId = 0) =>
		await SqlDataAccess.LoadData<ReceiptPaymentModel, dynamic>("LoadPaymentsById", new { TransactionId = transactionId, AdvanceId = advanceId });
}