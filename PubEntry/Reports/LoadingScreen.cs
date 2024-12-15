namespace PubEntry.Reports;

public partial class LoadingScreen : Form
{
	public LoadingScreen()
	{
		InitializeComponent();
	}

	private static LoadingScreen loadingScreen;

	static public void ShowSplashScreen()
	{
		if (loadingScreen != null) return;
		loadingScreen = new LoadingScreen();
		Thread thread = new Thread(new ThreadStart(ShowForm));
		thread.IsBackground = true;
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private static void ShowForm()
	{
		if (loadingScreen != null) Application.Run(loadingScreen);
	}

	public static void CloseForm() => loadingScreen?.Invoke(new CloseDelegate(CloseFormInternal));

	private static void CloseFormInternal()
	{
		if (loadingScreen != null)
		{
			loadingScreen.Close();
			loadingScreen = null;
		};
	}

	private delegate void CloseDelegate();
}
