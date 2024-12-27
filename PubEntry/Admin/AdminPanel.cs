namespace PubEntry.Admin;

public partial class AdminPanel : Form
{
	public AdminPanel()
	{
		InitializeComponent();
	}

	private void manageEmployeeButton_Click(object sender, EventArgs e)
	{
		EmployeeForm employeeForm = new();
		employeeForm.ShowDialog();
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
