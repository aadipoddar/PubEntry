using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class EmployeeForm : Form
{
	public EmployeeForm()
	{
		InitializeComponent();

		Task task = LoadComboBox();
	}

	private async Task LoadComboBox()
	{
		locationComboBox.DataSource = null;

		var locations = (await CommonData.LoadTableData<LocationModel>("LocationTable")).ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.DisplayMember = "Name";
		locationComboBox.ValueMember = "Id";

		var employees = (await CommonData.LoadTableData<EmployeeModel>("EmployeeTable")).ToList();

		employees.Insert(0, new());

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
		employeeComboBox.ValueMember = "Name";

		statusComboBox.SelectedIndex = 0;
	}

	private bool ValidateForm()
	{
		if (employeeNameTextBox.Text == string.Empty) return false;
		if (passwordTextBox.Text == string.Empty) return false;

		return true;
	}

	private void ClearForm()
	{
		employeeNameTextBox.Text = string.Empty;
		passwordTextBox.Text = string.Empty;
		locationComboBox.SelectedIndex = 0;
		statusComboBox.SelectedIndex = 0;
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm()) return;

		if (employeeComboBox.SelectedIndex > 0)
		{
			EmployeeModel employeeModel = new();
			employeeModel.Name = employeeNameTextBox.Text;
			employeeModel.Password = passwordTextBox.Text;
			employeeModel.LocationId = (int)locationComboBox.SelectedValue;
			employeeModel.ActiveStatus = statusComboBox.SelectedIndex;

			await EmployeeData.UpdateEmployeeTableData(employeeModel, (employeeComboBox.SelectedItem as EmployeeModel).Id);
		}

		else
		{
			EmployeeModel employeeModel = new();
			employeeModel.Name = employeeNameTextBox.Text;
			employeeModel.Password = passwordTextBox.Text;
			employeeModel.LocationId = (int)locationComboBox.SelectedValue;
			employeeModel.ActiveStatus = statusComboBox.SelectedIndex;

			await EmployeeData.InsertEmployeeTableData(employeeModel);
		}

		ClearForm();
		employeeComboBox.SelectedIndex = 0;
	}

	private void employeeComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (employeeComboBox.SelectedIndex > 0)
		{
			EmployeeModel employee = employeeComboBox.SelectedItem as EmployeeModel;
			employeeNameTextBox.Text = employee.Name;
			passwordTextBox.Text = employee.Password;
			locationComboBox.SelectedValue = employee.LocationId;
			statusComboBox.SelectedIndex = employee.ActiveStatus;
		}

		else ClearForm();
	}
}
