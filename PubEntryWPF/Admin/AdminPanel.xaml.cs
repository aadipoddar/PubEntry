using System.Windows;

using PubEntryWPF.Transaction;
using PubEntryWPF.Transaction.Advance;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for AdminPanel.xaml
/// </summary>
public partial class AdminPanel : Window
{
	private readonly Dashboard _dashboard;

	public AdminPanel(Dashboard dashboard)
	{
		InitializeComponent();
		_dashboard = dashboard;
	}

	private void Window_Loaded(object sender, RoutedEventArgs e) => mainFrame.Content = new SlipIdPage(mainFrame);

	private async void Window_Closed(object sender, EventArgs e)
	{
		_dashboard.Show();
		_dashboard.passwordBox.Focus();
		await _dashboard.LoadLocationComboBox();
		Close();
	}

	private void manageTransactionButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new SlipIdPage(mainFrame);

	private void updateAdvanceButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new AdvanceIdPage(mainFrame);

	private void manageUsersButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new UserPage();

	private void manageLocationsButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new LocationPage();

	private void managePaymentModeButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new PaymentModePage();

	private void manageReservationsButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new ReservationTypePage();

	private void managePersonButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new PersonPage();

	private void sqlEditorButton_Click(object sender, RoutedEventArgs e) => mainFrame.Content = new SqlEditorPage();
}
