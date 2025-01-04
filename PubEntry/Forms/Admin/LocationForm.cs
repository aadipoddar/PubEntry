﻿namespace PubEntry.Forms.Admin;

public partial class LocationForm : Form
{
	public LocationForm() => InitializeComponent();

	private void LocationForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableData<LocationModel>("LocationTable");
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		locationComboBox.SelectedIndex = -1;
	}

	private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (locationComboBox.SelectedItem is LocationModel selectedLocation)
		{
			nameTextBox.Text = selectedLocation.Name;
			statusCheckBox.Checked = selectedLocation.Status;
		}

		else
		{
			nameTextBox.Text = string.Empty;
			statusCheckBox.Checked = true;
		}
	}

	private bool ValidateForm()
	{
		if (nameTextBox.Text == string.Empty) return false;

		return true;
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter a location name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		LocationModel locationModel = new()
		{
			Name = nameTextBox.Text,
			Status = statusCheckBox.Checked
		};

		if (locationComboBox.SelectedIndex == -1) await LocationData.LocationInsert(locationModel);
		else
		{
			locationModel.Id = (locationComboBox.SelectedItem as LocationModel).Id;
			await LocationData.LocationUpdate(locationModel);
		}

		LoadData();
	}
}
