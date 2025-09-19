namespace PubReport;

public static class CreateNotification
{
	public static async Task<(string title, string text)> CreateNotificationText()
	{
		if (DateTime.Now.Hour < int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)) &&
			DateTime.Now.Hour > int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)) + 1)
		{
			SecureStorage.RemoveAll();
			return (null, null);
		}

		var transactionTotalsModels = await LoadTransactionTotals();

		if (DateTime.Now.Hour == int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)) && DateTime.Now.Minute <= 15)
		{
			SecureStorage.RemoveAll();
			return ("Pub Entry Report", await GetNotificationContent(transactionTotalsModels));
		}

		var backgroundServiceLocationMark = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceLocationMark));
		foreach (var transactionTotalsModel in transactionTotalsModels)
		{
			var locationMark = await SecureStorage.GetAsync($"Location_{transactionTotalsModel.LocationId}_Mark");
			var amount = transactionTotalsModel.Card + transactionTotalsModel.Cash + transactionTotalsModel.UPI + transactionTotalsModel.Amex + transactionTotalsModel.OnlineQR;

			if (string.IsNullOrWhiteSpace(locationMark))
				await SecureStorage.SetAsync($"Location_{transactionTotalsModel.LocationId}_Mark", amount.ToString());

			else if (amount - int.Parse(locationMark) >= backgroundServiceLocationMark)
			{
				var location = await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, transactionTotalsModel.LocationId);
				await SecureStorage.SetAsync($"Location_{transactionTotalsModel.LocationId}_Mark", amount.ToString());
				return ($"{location.Name} Crossed ₹{amount}+", await GetNotificationContent(transactionTotalsModels));
			}
		}

		var grandTotalMark = await SecureStorage.GetAsync("GrandTotal_Mark");
		var backgroundServiceGrandTotalMark = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceGrandTotalMark));
		var grandTotalAmount = transactionTotalsModels.Sum(x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR);

		if (string.IsNullOrWhiteSpace(grandTotalMark))
			await SecureStorage.SetAsync("GrandTotal_Mark", grandTotalAmount.ToString());

		else if (grandTotalAmount - int.Parse(grandTotalMark) >= backgroundServiceGrandTotalMark)
		{
			await SecureStorage.SetAsync("GrandTotal_Mark", grandTotalAmount.ToString());
			return ($"Grand Total Crossed ₹{grandTotalAmount}+", await GetNotificationContent(transactionTotalsModels));
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