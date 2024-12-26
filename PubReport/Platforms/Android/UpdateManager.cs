﻿using Android.Content;

using Application = Android.App.Application;

namespace PubReport.Platforms.Android;

public static class AadiSoftUpdater
{
	public static async Task<bool> CheckForUpdates(string githubRepoOwner, string githubRepoName, string currentVersion)
	{
		string fileContent = await GetLatestVersionFromGithub(githubRepoOwner, githubRepoName);
		if (fileContent.Contains("Android Latest Version = "))
		{
			string latestVersion = fileContent.Substring(fileContent.IndexOf("Android Latest Version = ") + 25, 7);
			return latestVersion != currentVersion;
		}
		return false;
	}

	private static async Task<string> GetLatestVersionFromGithub(string githubRepoOwner, string githubRepoName)
	{
		string fileUrl = $"https://raw.githubusercontent.com/{githubRepoOwner}/{githubRepoName}/refs/heads/main/README.md";
		using (HttpClient client = new HttpClient())
			return await client.GetStringAsync(fileUrl);
	}

	public static async Task UpdateApp(string githubRepoOwner, string githubRepoName, string setupAPKName)
	{
		var url = $"https://github.com/{githubRepoOwner}/{githubRepoName}/releases/latest/download/{setupAPKName}.apk";
		var filePath = Path.Combine(Application.Context.GetExternalFilesDir(null).AbsolutePath, $"{setupAPKName}.apk");

		using (HttpClient client = new HttpClient())
		using (var response = await client.GetAsync(url))
		using (var stream = await response.Content.ReadAsStreamAsync())
		using (var fileStream = new FileStream(filePath, FileMode.Create))
		{
			await stream.CopyToAsync(fileStream);
		}

		InstallAPK(filePath);
	}

	private static void InstallAPK(string filePath)
	{
		var file = new Java.IO.File(filePath);
		var fileUri = AndroidX.Core.Content.FileProvider.GetUriForFile(Application.Context, Application.Context.PackageName + ".provider", file);
		var intent = new Intent(Intent.ActionView);
		intent.SetData(fileUri);
		intent.AddFlags(ActivityFlags.NewTask);
		intent.AddFlags(ActivityFlags.GrantReadUriPermission);
		Application.Context.StartActivity(intent);
	}
}
