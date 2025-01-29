using System.Reflection;
namespace PubEntry.Forms.Admin;

public partial class LocationForm : Form
{
	public LocationForm() => InitializeComponent();

	private void LocationForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableData<LocationModel>(Table.Location);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		locationComboBox.SelectedIndex = -1;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
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

	private bool ValidateForm() => !string.IsNullOrEmpty(nameTextBox.Text);

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

		if (locationComboBox.SelectedIndex == -1) await LocationData.InsertLocation(locationModel);
		else
		{
			locationModel.Id = (locationComboBox.SelectedItem as LocationModel).Id;
			await LocationData.Update_Location(locationModel);
		}

		LoadData();
	}
}
