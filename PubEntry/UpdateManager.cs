using System.Diagnostics;
using System.Reflection;

namespace PubEntry;

public static class UpdateManager
{
	private static async Task<string> GetLatestVersionFromGithub()
	{
		string fileUrl = "https://raw.githubusercontent.com/aadipoddar/PubEntry/refs/heads/main/README.md";

		using (HttpClient client = new HttpClient())
		{
			string cacheBuster = DateTime.UtcNow.Ticks.ToString();
			string requestUrl = $"{fileUrl}?cb={cacheBuster}";
			return await client.GetStringAsync(requestUrl);
		}
	}

	private static void UpdateApp(string filePath)
	{
		string batchFilePath = Path.Combine(Path.GetTempPath(), "update.bat");

		string batchScript = $@"
@echo off
echo Uninstalling program...
msiexec /x {{477557B4-2908-4106-B360-D2D114F02452}} /qb

echo Starting setup file...
start """" ""{filePath}""
";

		File.WriteAllText(batchFilePath, batchScript);
		Process.Start(new ProcessStartInfo(batchFilePath) { UseShellExecute = true });
		Environment.Exit(0);
	}

	private static async Task DownloadLatestVersion()
	{
		var url = "https://github.com/aadipoddar/PubEntry/releases/latest/download/PubEntrySetup.msi";
		var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PubEntrySetup.msi");

		using (HttpClient client = new HttpClient())
		using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
		using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
		using (Stream streamToWriteTo = File.Open(filePath, FileMode.Create))
			await streamToReadFrom.CopyToAsync(streamToWriteTo);


		UpdateApp(filePath);
	}

	public static async Task CheckForUpdates()
	{
		string fileContent = await GetLatestVersionFromGithub();

		if (fileContent.Contains("Latest Version = "))
		{
			string latestVersion = fileContent.Substring(fileContent.IndexOf("Latest Version = ") + 17, 7);
			string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			if (latestVersion != currentVersion)
				if (MessageBox.Show("New Version Available. Do you want to update?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
					await DownloadLatestVersion();
		}
	}
}
