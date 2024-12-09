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

		if (DateTime.Now.Hour > 4)
			fromTimeTextBox.Text = (DateTime.Now.Hour - 3).ToString();

		else fromTimeTextBox.Text = DateTime.Now.Hour.ToString();

		toTimeTextBox.Text = DateTime.Now.Hour.ToString();
	}

	private bool ValidatePassword()
	{
		var employeeId = employeeComboBox.SelectedIndex;
		if (Task.Run(async () => await DataAccess.GetEmployeePasswordById(employeeId)).Result == passwordTextBox.Text)
			return true;
		return false;
	}

	private bool ValidateTime()
	{
		if (Convert.ToInt64(toTimeTextBox.Text) > Convert.ToInt64(fromTimeTextBox.Text))
			return true;

		else return false;
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		if (ValidatePassword())
		{
			MainForm mainForm = new(locationComboBox.SelectedIndex, employeeComboBox.SelectedIndex);
			mainForm.Show();
			Hide();
		}

		else MessageBox.Show("Incorrect Password");
	}

	private void fromTimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private void summaryReportButton_Click(object sender, EventArgs e)
	{
		if (ValidatePassword() && ValidateTime())
		{
			ShowDataForm showDataForm = new ShowDataForm(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox);
			showDataForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Password or Time");
	}
}
