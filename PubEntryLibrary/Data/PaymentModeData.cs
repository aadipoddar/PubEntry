namespace PubEntryLibrary.Data;

public class PaymentModeData
{
	public static async Task InsertPaymentMode(PaymentModeModel paymentModeModel) =>
			await SqlDataAccess.SaveData(StoredProcedureNames.InsertPaymentMode, paymentModeModel);
}