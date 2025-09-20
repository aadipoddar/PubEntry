using Android.App;
using Android.Content;
using Android.Runtime;

using AndroidX.Core.Content;

namespace PubReport.Platforms.Android;

[BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
[IntentFilter([Intent.ActionBootCompleted])]
[Register("pubreport.platforms.android.MyBootReceiver")]
public class MyBootReceiver : BroadcastReceiver
{
	public override void OnReceive(Context context, Intent intent)
	{
		if (intent.Action == Intent.ActionBootCompleted)
		{
			// Toast.MakeText(context, "Pub Entry is Running in the Background", ToastLength.Short).Show();

			var serviceIntent = new Intent(context, typeof(MyBackgroundService));
			ContextCompat.StartForegroundService(context, serviceIntent);
		}
	}
}