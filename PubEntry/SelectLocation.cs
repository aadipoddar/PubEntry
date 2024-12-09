using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class SelectLocation : Form
{
	public SelectLocation()
	{
		InitializeComponent();

		Task task = LoadComboBox();
	}

	public async Task LoadComboBox()
	{
		locationComboBox.DataSource = null;
		employeeComboBox.DataSource = null;

		var locations = (await DataAccess.LoadTableData<LocationModel>("LocationTable")).ToList();
		var employees = (await DataAccess.LoadTableData<EmployeeModel>("EmployeeTable")).ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.DisplayMember = "Name";

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		var employeeId = employeeComboBox.SelectedIndex;
		if (Task.Run(async () => await DataAccess.GetEmployeePasswordById(employeeId)).Result == passwordTextBox.Text)
		{
			MainForm mainForm = new(locationComboBox.SelectedIndex, employeeComboBox.SelectedIndex);
			mainForm.Show();
			Hide();
		}

		else
			MessageBox.Show("Incorrect Password");
	}

	private void showDataButton_Click(object sender, EventArgs e)
	{
		ShowDataForm showDataForm = new ShowDataForm(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox);
		showDataForm.ShowDialog();
	}
}
