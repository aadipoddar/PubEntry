namespace PubEntryLibrary.Data;

public class PaymentModeData
{
	public static async Task PaymentModeInsert(PaymentModeModel paymentModeModel) =>
			await SqlDataAccess.SaveData("PaymentModeInsert", paymentModeModel);

	public static async Task PaymentModeUpdate(PaymentModeModel paymentModeModel) =>
		await SqlDataAccess.SaveData("PaymentModeUpdate", paymentModeModel);
}