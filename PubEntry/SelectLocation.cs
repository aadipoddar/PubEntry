using System.Reflection;

using PubEntry.Reports;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class SelectLocation : Form
{
	#region InitialLoading
	public SelectLocation()
	{
		InitializeComponent();
	}

	private async void SelectLocation_Load(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();
		LoadComboBox();
		LoadingScreen.CloseForm();
		await UpdateManager.CheckForUpdates();
	}

	public void LoadComboBox()
	{
		locationComboBox.DataSource = null;
		List<LocationModel> locations = new();

		try
		{
			locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();
		}
		catch (Exception)
		{
			MessageBox.Show("DateBase is Starting Please Try after SomeTime");
			Application.Exit();
		}

		locationComboBox.DataSource = locations;
		locationComboBox.ValueMember = "Id";
		locationComboBox.DisplayMember = "Name";

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

		LoadEmployeeComboBox();
	}

	private async void LoadEmployeeComboBox()
	{
		if (locationComboBox.ValueMember != "Id") return;
		List<EmployeeModel> employees = new();

		employeeComboBox.DataSource = null;

		try
		{
			employees = (await EmployeeData.LoadActiveEmployeeByLocation((int)locationComboBox.SelectedValue)).ToList();
		}
		catch (Exception)
		{
			MessageBox.Show("DateBase is Starting Please Try after SomeTime");
			Application.Exit();
		}

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
		employeeComboBox.ValueMember = "Id";
	}

	private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		LoadEmployeeComboBox();
		passwordTextBox.Text = string.Empty;
	}

	private void employeeComboBox_SelectedIndexChanged(object sender, EventArgs e) => passwordTextBox.Text = string.Empty;
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
		SelectDataForm selectDataForm = new((int)locationComboBox.SelectedValue);
		selectDataForm.Show();
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
