using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using AndroidX.Core.App;

namespace PubReport.Platforms.Android;

[Service]
public class MyBackgroundService : Service
{
	Timer timer = null;
	int myId = (new object()).GetHashCode();
	int BadgeNumber = 0;
	private readonly IBinder binder = new LocalBinder();

	public class LocalBinder : Binder
	{
		public MyBackgroundService GetService() =>
			GetService();
	}

	public override IBinder OnBind(Intent intent) =>
		binder;

	public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
	{
		var input = intent.GetStringExtra("inputExtra");

		var notificationIntent = new Intent(this, typeof(MainActivity));
		notificationIntent.SetAction("USER_TAPPED_NOTIFIACTION");

		var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
			PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

		var notification = new NotificationCompat.Builder(this, MainApplication.ChannelId)
			.SetContentText(input)
			.SetSmallIcon(Resource.Drawable.AppIcon)
			.SetContentIntent(pendingIntent);

		if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake) // API 34+
			StartForeground(myId, notification.Build(), ForegroundService.TypeSpecialUse);
		else
			StartForeground(myId, notification.Build());

		// Start the timer initialization in a background task
		_ = Task.Run(async () => await InitializeTimerAsync(notification));

		// You can stop the service from inside the service by calling StopSelf();

		return StartCommandResult.Sticky;
	}

	private async Task InitializeTimerAsync(NotificationCompat.Builder notification)
	{
		try
		{
			int backgroundServiceTimer = await GetBackgroundServiceTimerAsync();
			timer = new Timer(Timer_Elapsed, notification, 0, backgroundServiceTimer * 60 * 1000);
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Error initializing timer: {ex.Message}");
			// Use default timer value
			timer = new Timer(Timer_Elapsed, notification, 0, 5 * 60 * 1000); // 5 minutes default
		}
	}

	private async Task<int> GetBackgroundServiceTimerAsync()
	{
		try
		{
			var settingValue = await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceTimer);

			if (string.IsNullOrWhiteSpace(settingValue))
				return 5; // Default to 5 minutes

			if (int.TryParse(settingValue, out int timerValue) && timerValue > 0)
				return timerValue;
			else
				return 5; // Default to 5 minutes for invalid values
		}
		catch
		{
			return 5; // Default to 5 minutes on any error
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="state"></param>
	void Timer_Elapsed(object state)
	{
		AndroidServiceManager.IsRunning = true;

		BadgeNumber++;

		var (title, text) = CreateNotification.CreateNotificationText().Result;

		if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(text))
			return;

		var notification = (NotificationCompat.Builder)state;
		notification.SetNumber(BadgeNumber);
		notification.SetContentTitle(title);
		notification.SetContentText(text);

		// Apply the same foreground service type logic here
		if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake) // API 34+
			StartForeground(myId, notification.Build(), ForegroundService.TypeSpecialUse);
		else
			StartForeground(myId, notification.Build());
	}
}