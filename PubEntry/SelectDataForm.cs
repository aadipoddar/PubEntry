namespace PubEntry;

public partial class SelectDataForm : Form
{
	int locationId;

	public SelectDataForm(int locationId)
	{
		InitializeComponent();

		this.locationId = locationId;
	}

	#region Validation
	private void timeTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private void timeTextBox_TextChanged(object sender, EventArgs e)
	{
		if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
			if (int.TryParse(textBox.Text, out int time))
				if (time > 23)
					textBox.Text = "23";
	}
	#endregion

	private void summaryReportButton_Click(object sender, EventArgs e)
	{
		ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox);
		showDataForm.ShowDialog();
	}

	private void detailReportButton_Click(object sender, EventArgs e)
	{
		ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox, locationId, true);
		showDataForm.ShowDialog();
	}
}
