using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class SelectLocationDetailPrint : Form
{
	public int SelectedLocationId { get; private set; }

	public SelectLocationDetailPrint()
	{
		InitializeComponent();

		LoadComboBox();
	}

	private void LoadComboBox()
	{
		locationComboBox.DataSource = null;

		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.ValueMember = "Id";
		locationComboBox.DisplayMember = "Name";
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		SelectedLocationId = (int)locationComboBox.SelectedValue;

		DialogResult = DialogResult.OK;
		Close();
	}
}
