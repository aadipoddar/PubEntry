namespace PubEntry.Forms.Reports;

public partial class LoadingScreen : Form
{
	private static LoadingScreen? _loadingScreen;

	public LoadingScreen() => InitializeComponent();

	public static void ShowSplashScreen()
	{
		if (_loadingScreen != null) return;
		_loadingScreen = new LoadingScreen();
		Thread thread = new(ShowForm)
		{
			IsBackground = true
		};
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private static void ShowForm()
	{
		if (_loadingScreen != null) Application.Run(_loadingScreen);
	}

	public static void CloseForm() => _loadingScreen?.Invoke(new Action(CloseFormInternal));

	private static void CloseFormInternal()
	{
		_loadingScreen?.Close();
		_loadingScreen = null;
	}
}
