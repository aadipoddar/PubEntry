using System.Reflection;

using PubEntry.Admin;
using PubEntry.Reports;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class Dashboard : Form
{
	#region InitialLoading
	public Dashboard()
	{
		InitializeComponent();
	}

	private async void SelectLocation_Load(object sender, EventArgs e)
	{
		await UpdateCheck();
		LoadingScreen.ShowSplashScreen();
		LoadComboBox();
		LoadingScreen.CloseForm();
	}

	private async Task UpdateCheck()
	{
		bool isUpdateAvailable = await AadiSoftUpdater.AadiSoftUpdater.CheckForUpdates("aadipoddar", "PubEntry", Assembly.GetExecutingAssembly().GetName().Version.ToString());

		if (isUpdateAvailable)
			if (MessageBox.Show("New Version Available. Do you want to update?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
				await AadiSoftUpdater.AadiSoftUpdater.UpdateApp("aadipoddar", "PubEntry", "PubEntrySetup", "477557B4-2908-4106-B360-D2D114F02452");
	}

	public void LoadComboBox()
	{
		locationComboBox.DataSource = null;
		List<LocationModel> locations = new();

		try
		{
			locations = Task.Run(LocationData.LoadActiveLocations).Result.ToList();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Database Error");
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
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Database Error");
			Application.Exit();
		}

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
		employeeComboBox.ValueMember = "Id";
	}
	#endregion

	#region Events
	private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		LoadEmployeeComboBox();
		passwordTextBox.Text = string.Empty;
	}

	private void employeeComboBox_SelectedIndexChanged(object sender, EventArgs e) => passwordTextBox.Text = string.Empty;

	private void goButton_Click(object sender, EventArgs e)
	{
		if (ValidatePassword())
		{
			EntryForm mainForm = new((int)locationComboBox.SelectedValue, (int)employeeComboBox.SelectedValue);
			mainForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Password");
	}

	private void reportsButton_Click(object sender, EventArgs e)
	{
		SelectDataForm selectDataForm = new((int)locationComboBox.SelectedValue);
		selectDataForm.Show();
	}

	private void manageEmployeeButton_Click(object sender, EventArgs e)
	{
		if (passwordTextBox.Text == "admin")
		{
			EmployeeForm employeeForm = new();
			employeeForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Password");
	}

	private void manageLocationButton_Click(object sender, EventArgs e)
	{
		if (passwordTextBox.Text == "admin")
		{
			LocationForm locationForm = new();
			locationForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Password");
	}
	#endregion

	#region Validation
	private bool ValidatePassword()
	{
		EmployeeModel employee = employeeComboBox.SelectedItem as EmployeeModel;

		if (employee.Password == passwordTextBox.Text || passwordTextBox.Text == "admin")
		{
			passwordTextBox.Text = string.Empty;
			return true;
		}

		passwordTextBox.Text = string.Empty;
		return false;
	}
	#endregion
}
