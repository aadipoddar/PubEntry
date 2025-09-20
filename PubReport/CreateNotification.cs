using System.Text;

namespace PubReport;

public static class CreateNotification
{
	public static async Task<(string title, string text)> CreateNotificationText()
	{
		try
		{
			var openTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime));
			var closeTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime));

			if (DateTime.Now.Hour < openTime && DateTime.Now.Hour > closeTime + 1)
			{
				SecureStorage.RemoveAll();
				return (null, null);
			}

			var transactionTotalsModels = await LoadTransactionTotals(openTime, closeTime);

			// Pre-calculate amounts to avoid repeated calculations
			var locationAmounts = transactionTotalsModels.ToDictionary(
				x => x.LocationId,
				x => x.Card + x.Cash + x.UPI + x.Amex + x.OnlineQR);

			#region EOD Report
			if (DateTime.Now.Hour == closeTime && DateTime.Now.Minute <= 15)
			{
				SecureStorage.RemoveAll();
				return ("Pub Entry Report", await GetNotificationContent(transactionTotalsModels, locationAmounts));
			}
			#endregion

			#region Location Wise
			var backgroundServiceLocationMark = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceLocationMark));

			// Batch load all location marks at once
			var locationMarkTasks = locationAmounts.Keys.Select(async locationId =>
				new { LocationId = locationId, Mark = await SecureStorage.GetAsync($"Location_{locationId}_Mark") });
			var locationMarks = await Task.WhenAll(locationMarkTasks);

			foreach (var markInfo in locationMarks)
			{
				var amount = locationAmounts[markInfo.LocationId];

				if (string.IsNullOrWhiteSpace(markInfo.Mark))
					await SecureStorage.SetAsync($"Location_{markInfo.LocationId}_Mark", amount.ToString());

				else if (amount - int.Parse(markInfo.Mark) >= backgroundServiceLocationMark)
				{
					var location = await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, markInfo.LocationId);
					await SecureStorage.SetAsync($"Location_{markInfo.LocationId}_Mark", amount.ToString());
					return ($"{location.Name} Crossed ₹{amount}+", await GetNotificationContent(transactionTotalsModels, locationAmounts));
				}
			}
			#endregion

			#region Grand Total
			var grandTotalMark = await SecureStorage.GetAsync("GrandTotal_Mark");
			var backgroundServiceGrandTotalMark = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceGrandTotalMark));
			var grandTotalAmount = locationAmounts.Values.Sum();

			if (string.IsNullOrWhiteSpace(grandTotalMark))
				await SecureStorage.SetAsync("GrandTotal_Mark", grandTotalAmount.ToString());

			else if (grandTotalAmount - int.Parse(grandTotalMark) >= backgroundServiceGrandTotalMark)
			{
				await SecureStorage.SetAsync("GrandTotal_Mark", grandTotalAmount.ToString());
				return ($"Grand Total Crossed ₹{grandTotalAmount}+", await GetNotificationContent(transactionTotalsModels, locationAmounts));
			}
			#endregion

			return (null, null);
		}
		catch (Exception ex)
		{
			return ("Error Occurred", $"{ex.Message}");
		}
	}

	private static async Task<string> GetNotificationContent(
		List<TransactionTotalsModel> transactionTotalsModels,
		Dictionary<int, int> locationAmounts)
	{
		var content = new StringBuilder();

		// Batch load all locations at once
		var locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		var locationLookup = locations.ToDictionary(x => x.Id, x => x.Name);

		foreach (var transactionTotalsModel in transactionTotalsModels)
		{
			var locationName = locationLookup[transactionTotalsModel.LocationId];
			var amount = locationAmounts[transactionTotalsModel.LocationId];
			content.Append($"{locationName}: ₹{amount} | ");
		}

		var grandTotal = locationAmounts.Values.Sum();
		content.Append($"Grand Total: ₹{grandTotal}");

		return content.ToString();
	}

	private static async Task<List<TransactionTotalsModel>> LoadTransactionTotals(int openTime, int closeTime)
	{
		DateTime fromDate, toDate;

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

		var locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);

		// Batch load all transaction totals at once if possible
		var transactionTotalTasks = locations.Select(async location =>
		{
			var transactionTotal = await TransactionData.LoadTransactionTotalsByDateLocation(fromDate, toDate, location.Id);
			return transactionTotal ?? new TransactionTotalsModel { LocationId = location.Id };
		});

		return [.. (await Task.WhenAll(transactionTotalTasks))];
	}
}