namespace PubEntry.Admin;

public partial class AdminPanel : Form
{
	public AdminPanel() => InitializeComponent();

	private void advanceButton_Click(object sender, EventArgs e)
	{
		AdvanceEntryForm advanceForm = new();
		advanceForm.ShowDialog();
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

	private void manageTransactionsButton_Click(object sender, EventArgs e)
	{
		SlipIdForm slipIdForm = new();
		slipIdForm.ShowDialog();
	}
}
