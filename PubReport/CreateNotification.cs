namespace PubEntryLibrary.Data;

public static class CreateNotification
{
	public static async Task<(string title, string text)> CreateNotificationText()
	{
		if (DateTime.Now.Hour < int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)) &&
			DateTime.Now.Hour > int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)) + 1)
			return (null, null);

		var transactionTotalsModels = await LoadTransactionTotals();

		if (DateTime.Now.Hour == int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)) && DateTime.Now.Minute <= 15)
		{
			foreach (var _ in transactionTotalsModels)
				SecureStorage.RemoveAll();

			return ("Pub Entry Report", await GetNotificationContent(transactionTotalsModels));
		}

		foreach (var transactionTotalsModel in transactionTotalsModels)
		{
			var locationMark = await SecureStorage.GetAsync($"Location_{transactionTotalsModel.LocationId}_Mark");

			if (string.IsNullOrWhiteSpace(locationMark))
				await SecureStorage.SetAsync($"Location_{transactionTotalsModel.LocationId}_Mark", (transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR).ToString());

			else if (transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR - int.Parse(locationMark) >= 25000)
			{
				await SecureStorage.SetAsync($"Location_{transactionTotalsModel.LocationId}_Mark", (transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR).ToString());

				var location = await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, transactionTotalsModel.LocationId);
				return ($"{location.Name} Crossed ₹{transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR}+", await GetNotificationContent(transactionTotalsModels));
			}
		}

		var grandTotalMark = await SecureStorage.GetAsync("GrandTotal_Mark");

		if (string.IsNullOrWhiteSpace(grandTotalMark))
			await SecureStorage.SetAsync("GrandTotal_Mark", transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR).ToString());

		else if (transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR) - int.Parse(grandTotalMark) >= 50000)
		{
			await SecureStorage.SetAsync("GrandTotal_Mark", transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR).ToString());
			return ($"Grand Total Crossed ₹{transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR)}+", await GetNotificationContent(transactionTotalsModels));
		}

		return (null, null);
	}

	private static async Task<string> GetNotificationContent(List<TransactionTotalsModel> transactionTotalsModels)
	{
		var content = "";

		foreach (var transactionTotalsModel in transactionTotalsModels)
		{
			var location = await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, transactionTotalsModel.LocationId);
			content += $"{location.Name}: ₹{transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR} | ";
		}

		content += $"Grand Total: ₹{transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR)}";
		return content;
	}

	private static async Task<List<TransactionTotalsModel>> LoadTransactionTotals()
	{
		DateTime fromDate, toDate;

		var openTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime));
		var closeTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime));

		if (DateTime.Now.Hour >= openTime)
		{
			fromDate = DateTime.Now;
			toDate = DateTime.Now.AddDays(1);
		}
		else
		{
			fromDate = DateTime.Now.AddDays(-1);
			toDate = DateTime.Now;
		}

		fromDate = fromDate.Date.AddHours(openTime);
		toDate = toDate.Date.AddHours(closeTime);

		List<TransactionTotalsModel> transactionTotalsModels = [];

		foreach (var location in await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location))
		{
			var transactionTotal = await TransactionData.LoadTransactionTotalsByDateLocation(fromDate, toDate, location.Id);
			transactionTotal ??= new TransactionTotalsModel { LocationId = location.Id };
			transactionTotalsModels.Add(transactionTotal);
		}

		return transactionTotalsModels;
	}
}