namespace PubEntryLibrary.Data;

public class PaymentModeData
{
	public static async Task InsertPaymentMode(PaymentModeModel paymentModeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.InsertPaymentMode, paymentModeModel);

	public static async Task UpdatePaymentMode(PaymentModeModel paymentModeModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdatePaymentMode, paymentModeModel);
}