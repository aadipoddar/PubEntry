using System.Reflection;

using PubEntry.Forms.Admin;
using PubEntry.Forms.Reports;
using PubEntry.Forms.Transaction;

namespace PubEntry;

public partial class Dashboard : Form
{
	#region InitialLoading
	public Dashboard()
	{
		InitializeComponent();
		UpdateCheck();
	}

	private static async void UpdateCheck()
	{
		bool isUpdateAvailable = await AadiSoftUpdater.AadiSoftUpdater.CheckForUpdates("aadipoddar", "PubEntry", Assembly.GetExecutingAssembly().GetName().Version.ToString());

		if (isUpdateAvailable)
			if (MessageBox.Show("New Version Available. Do you want to update?", "Update Available", MessageBoxButtons.YesNo) == DialogResult.Yes)
				await AadiSoftUpdater.AadiSoftUpdater.UpdateApp("aadipoddar", "PubEntry", "PubEntrySetup", "477557B4-2908-4106-B360-D2D114F02452");
	}

	private async void SelectLocation_Load(object sender, EventArgs e) => await LoadLocationComboBox();

	private async Task LoadLocationComboBox()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location, true);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private async Task LoadUserComboBox()
	{
		userComboBox.DataSource = await UserData.LoadUsersByLocationId((locationComboBox.SelectedItem as LocationModel).Id);
		userComboBox.DisplayMember = nameof(UserModel.Name);
		userComboBox.ValueMember = nameof(UserModel.Id);
	}
	#endregion

	private bool ValidatePassword()
	{
		if ((userComboBox.SelectedItem as UserModel)?.Password == passwordTextBox.Text)
		{
			passwordTextBox.Text = string.Empty;
			return true;
		}

		passwordTextBox.Text = string.Empty;
		return false;
	}

	#region Events
	private async void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) => await LoadUserComboBox();

	private void userComboBox_SelectedIndexChanged(object sender, EventArgs e) => passwordTextBox.Text = string.Empty;

	private void goButton_Click(object sender, EventArgs e)
	{
		if (!ValidatePassword())
		{
			MessageBox.Show("Incorrect Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		TransactionForm entryForm = new((locationComboBox.SelectedItem as LocationModel).Id, (userComboBox.SelectedItem as UserModel).Id);
		entryForm.ShowDialog();
	}

	private void reportsButton_Click(object sender, EventArgs e)
	{
		SelectDataForm selectDataForm = new();
		selectDataForm.ShowDialog();
	}

	private void adminButton_Click(object sender, EventArgs e)
	{
		if (!ValidatePassword() || !(userComboBox.SelectedItem as UserModel).Admin)
		{
			MessageBox.Show("Incorrect Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		AdminPanel adminPanel = new();
		adminPanel.ShowDialog();
	}
	#endregion
}
