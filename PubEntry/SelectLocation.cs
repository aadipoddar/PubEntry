using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class SelectLocation : Form
{
	#region InitialLoading
	public SelectLocation()
	{
		InitializeComponent();

		LoadComboBox();
	}

	public void LoadComboBox()
	{
		locationComboBox.DataSource = null;

		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.ValueMember = "Id";
		locationComboBox.DisplayMember = "Name";

		LoadEmployeeComboBox();
	}

	private async void LoadEmployeeComboBox()
	{
		if (locationComboBox.ValueMember != "Id") return;

		employeeComboBox.DataSource = null;

		var employees = (await EmployeeData.LoadEmployeeByLocation((int)locationComboBox.SelectedValue)).ToList();

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
		employeeComboBox.ValueMember = "Id";
	}

	private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) => LoadEmployeeComboBox();
	#endregion

	#region ClickEvents
	private void goButton_Click(object sender, EventArgs e)
	{
		if (ValidatePassword())
		{
			MainForm mainForm = new((int)locationComboBox.SelectedValue, (int)employeeComboBox.SelectedValue);
			mainForm.Show();
			Hide();
		}

		else MessageBox.Show("Incorrect Password");
	}

	private void reportsButton_Click(object sender, EventArgs e)
	{
		if (passwordTextBox.Text == "admin")
		{
			SelectDataForm selectDataForm = new();
			selectDataForm.Show();
		}

		else MessageBox.Show("Incorrect Password");
	}

	private void newEmployeeButton_Click(object sender, EventArgs e)
	{
		if (passwordTextBox.Text == "admin")
		{
			EmployeeForm employeeForm = new();
			employeeForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Password");
	}
	#endregion

	#region Validation
	private bool ValidatePassword()
	{
		var employeeId = employeeComboBox.SelectedValue;
		if (Task.Run(async () => await EmployeeData.GetEmployeePasswordById((int)employeeId)).Result == passwordTextBox.Text || passwordTextBox.Text == "admin")
			return true;
		return false;
	}
	#endregion
}
