#if DEBUG
	using Microsoft.Extensions.Logging;
#endif

using PubEntryLibrary.DataAccess;

using Syncfusion.Maui.Core.Hosting;

namespace PubReport;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionLicense);
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureSyncfusionCore()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
