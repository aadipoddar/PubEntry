namespace PubReport.Platforms.Android;

public static class AndroidServiceManager
{
	public static MainActivity MainActivity { get; set; }

	public static bool IsRunning { get; set; }

	public static void StartMyService()
	{
		if (MainActivity is null) return;
		MainActivity.StartService();
	}

	public static void StopMyService()
	{
		if (MainActivity is null) return;
		MainActivity.StopService();
		IsRunning = false;
	}
}
