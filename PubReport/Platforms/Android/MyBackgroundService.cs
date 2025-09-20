using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using AndroidX.Core.App;

namespace PubReport.Platforms.Android;

[Service]
public class MyBackgroundService : Service
{
	private Timer timer;
	private readonly int myId = (new object()).GetHashCode();
	private int BadgeNumber = 0;
	private IBinder binder;

	public class LocalBinder : Binder
	{
		private readonly MyBackgroundService service;

		public LocalBinder(MyBackgroundService service) =>
			this.service = service;

		public MyBackgroundService GetService() =>
			service;
	}

	public MyBackgroundService() =>
		binder = new LocalBinder(this);

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
			.SetContentIntent(pendingIntent)
			.SetOngoing(true) // Makes notification persistent
			.SetAutoCancel(false) // Prevents notification from being dismissed
			.SetPriority(NotificationCompat.PriorityHigh)
			.SetCategory(NotificationCompat.CategoryService);

		if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake) // API 34+
			StartForeground(myId, notification.Build(), ForegroundService.TypeSpecialUse);
		else
			StartForeground(myId, notification.Build());

		timer = new Timer(Timer_Elapsed, notification, 0, 20000);


		// Return START_STICKY to ensure the service is restarted if killed
		return StartCommandResult.Sticky;
	}

	/// <summary>
	/// Timer callback method for updating notifications periodically
	/// </summary>
	/// <param name="state">The notification builder passed as state</param>
	private async void Timer_Elapsed(object state)
	{
		AndroidServiceManager.IsRunning = true;

		BadgeNumber++;

		try
		{
			var (title, text) = await CreateNotification.CreateNotificationText();

			if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(text))
				return;

			var notification = (NotificationCompat.Builder)state;
			notification.SetNumber(BadgeNumber);
			notification.SetContentTitle(title);
			notification.SetContentText(text);
			notification.SetOngoing(true); // Keep notification persistent
			notification.SetAutoCancel(false);

			// Apply the same foreground service type logic here
			if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake) // API 34+
				StartForeground(myId, notification.Build(), ForegroundService.TypeSpecialUse);
			else
				StartForeground(myId, notification.Build());
		}
		catch (Exception)
		{
			// Ignore exceptions to prevent service crashes
		}
	}
}