
using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry.Admin;

public partial class LocationForm : Form
{
	public LocationForm()
	{
		InitializeComponent();

		Task task = LoadComboBox();
	}

	private async Task LoadComboBox()
	{
		locationComboBox.DataSource = null;

		var locations = (await CommonData.LoadTableData<LocationModel>("LocationTable")).ToList();

		locations.Insert(0, new());

		locationComboBox.DataSource = locations;
		locationComboBox.DisplayMember = "Name";
		locationComboBox.ValueMember = "Id";

		statusComboBox.SelectedIndex = 0;
	}

	private bool ValidateForm()
	{
		if (locationNameTextBox.Text == string.Empty) return false;

		return true;
	}

	private void ClearForm()
	{
		locationNameTextBox.Text = string.Empty;
		locationComboBox.SelectedIndex = 0;
		statusComboBox.SelectedIndex = 0;
	}

	private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (locationComboBox.SelectedIndex > 0)
		{
			LocationModel location = locationComboBox.SelectedItem as LocationModel;
			locationNameTextBox.Text = location.Name;
			statusComboBox.SelectedIndex = location.ActiveStatus;
		}

		else ClearForm();
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm()) return;

		if (locationComboBox.SelectedIndex > 0)
		{
			LocationModel locationModel = new();
			locationModel.Name = locationNameTextBox.Text;
			locationModel.ActiveStatus = statusComboBox.SelectedIndex;

			await LocationData.UpdateLocationTableData(locationModel, (locationComboBox.SelectedItem as LocationModel).Id);
		}

		else
		{
			LocationModel locationModel = new();
			locationModel.Name = locationNameTextBox.Text;
			locationModel.ActiveStatus = statusComboBox.SelectedIndex;

			await LocationData.InsertLocationTableData(locationModel);
		}

		ClearForm();
	}
}
