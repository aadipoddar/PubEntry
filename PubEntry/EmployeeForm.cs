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
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm()) return;

		EmployeeModel employeeModel = new();
		employeeModel.Name = employeeNameTextBox.Text;
		employeeModel.Password = passwordTextBox.Text;
		employeeModel.LocationId = (int)locationComboBox.SelectedValue;

		await EmployeeData.InsertEmployeeTableData(employeeModel);

		ClearForm();
	}
}
