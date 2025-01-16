namespace PubEntryLibrary.Data;

public class SettingsData
{
	public static async Task UpdateSettings(SettingsModel settingsModel) =>
			await SqlDataAccess.SaveData(StoredProcedure.UpdateSettings, settingsModel);

	public static async Task<string> LoadSettingsByKey(string Key) =>
			(await SqlDataAccess.LoadData<string, dynamic>(StoredProcedure.LoadSettingsByKey, new { Key })).FirstOrDefault();
}