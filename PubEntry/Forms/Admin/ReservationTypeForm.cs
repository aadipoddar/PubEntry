using System.Reflection;

namespace PubEntry.Forms.Admin;

public partial class ReservationTypeForm : Form
{
	public ReservationTypeForm() => InitializeComponent();

	private void ReservationTypeForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		reservationComboBox.DataSource = await CommonData.LoadTableData<ReservationTypeModel>(Table.ReservationType);
		reservationComboBox.DisplayMember = nameof(ReservationTypeModel.Name);
		reservationComboBox.ValueMember = nameof(ReservationTypeModel.Id);

		reservationComboBox.SelectedIndex = -1;

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private void reservationComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (reservationComboBox.SelectedItem is ReservationTypeModel selectedReservation)
		{
			nameTextBox.Text = selectedReservation.Name;
			statusCheckBox.Checked = selectedReservation.Status;
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

		ReservationTypeModel reservationTypeModel = new()
		{
			Name = nameTextBox.Text,
			Status = statusCheckBox.Checked
		};

		if (reservationComboBox.SelectedIndex == -1) await ReservationTypeData.InsertReservationType(reservationTypeModel);
		else
		{
			reservationTypeModel.Id = (reservationComboBox.SelectedItem as ReservationTypeModel).Id;
			await ReservationTypeData.Update_ReservationType(reservationTypeModel);
		}

		LoadData();
	}
}
