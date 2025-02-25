using System.Windows;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for AdminPanel.xaml
/// </summary>
public partial class AdminPanel : Window
{
	private readonly Dashboard _dashboard;
	private readonly int _userId, _locationId;

	public AdminPanel(Dashboard dashboard, int locationId, int userId)
	{
		InitializeComponent();
		_dashboard = dashboard;
		_locationId = locationId;
		_userId = userId;
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private async void Window_Closed(object sender, EventArgs e)
	{
		_dashboard.Show();
		_dashboard.passwordBox.Focus();
		await _dashboard.LoadLocationComboBox();
		Close();
	}

	private void manageTransactionButton_Click(object sender, RoutedEventArgs e)
	{

	}

	private void updateAdvanceButton_Click(object sender, RoutedEventArgs e)
	{

	}

	private void manageUsersButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new UserPage();

	private void manageLocationsButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new LocationPage();

	private void managePaymentModeButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new PaymentModePage();

	private void manageReservationsButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new ReservationTypePage();

}
