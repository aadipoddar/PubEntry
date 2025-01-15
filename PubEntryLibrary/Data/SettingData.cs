namespace PubEntryLibrary.Data;

public class SettingsData
{
	public static async Task UpdateSettings(SettingsModel settingsModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateSettings, settingsModel);
}