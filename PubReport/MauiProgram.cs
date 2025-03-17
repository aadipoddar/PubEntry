#if DEBUG
using Microsoft.Extensions.Logging;
#endif

using System.Reflection;

using Syncfusion.Maui.Core.Hosting;

#if ANDROID
using PubReport.Platforms.Android;
#endif

namespace PubReport;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionLicense);

		var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

#if ANDROID
		if (Task.Run(async () => await AadiSoftUpdater.CheckForUpdates("aadipoddar", "PubEntry", currentVersion)).Result)
			Task.Run(async () => await AadiSoftUpdater.UpdateApp("aadipoddar", "PubEntry", "com.aadisoft.pubreport"));
#endif
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.ConfigureSyncfusionCore();

		return builder.Build();
	}
}
