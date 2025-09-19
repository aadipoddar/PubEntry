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

		var backgroundServiceTimer = int.Parse(SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceTimer).Result);
		timer = new Timer(Timer_Elapsed, notification, 0, backgroundServiceTimer);

		// You can stop the service from inside the service by calling StopSelf();

		return StartCommandResult.Sticky;
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