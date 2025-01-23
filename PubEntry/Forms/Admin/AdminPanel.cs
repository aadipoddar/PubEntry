using System.Reflection;

using PubEntry.Forms.Transaction.Advance;

namespace PubEntry.Forms.Admin;

public partial class AdminPanel : Form
{
	public AdminPanel() => InitializeComponent();

	private void AdminPanel_Load(object sender, EventArgs e) =>
		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

	private void advanceButton_Click(object sender, EventArgs e)
	{
		AdvanceForm advanceForm = new();
		advanceForm.ShowDialog();
	}

	private void manageTransactionsButton_Click(object sender, EventArgs e)
	{
		SlipIdForm slipIdForm = new();
		slipIdForm.ShowDialog();
	}

	private void manageUsersButton_Click(object sender, EventArgs e)
	{
		UserForm userForm = new();
		userForm.ShowDialog();
	}

	private void manageLocationButton_Click(object sender, EventArgs e)
	{
		LocationForm locationForm = new();
		locationForm.ShowDialog();
	}

	private void managePersonsButton_Click(object sender, EventArgs e)
	{
		PersonForm personForm = new();
		personForm.ShowDialog();
	}

	private void paymentModeButton_Click(object sender, EventArgs e)
	{
		PaymentMode paymentModeForm = new();
		paymentModeForm.ShowDialog();
	}

	private void reservationButton_Click(object sender, EventArgs e)
	{
		ReservationTypeForm reservationTypeForm = new();
		reservationTypeForm.ShowDialog();
	}

	private void settingsButton_Click(object sender, EventArgs e)
	{
		SettingsForm settingForm = new();
		settingForm.ShowDialog();
	}

	private void sqlQueryButton_Click(object sender, EventArgs e)
	{
		SqlQuery sqlQuery = new();
		sqlQuery.ShowDialog();
	}
}
