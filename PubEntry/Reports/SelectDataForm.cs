using Microsoft.IdentityModel.Tokens;

namespace PubEntry.Reports;

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

	private bool ValidateTime()
	{
		if (string.IsNullOrEmpty(toTimeTextBox.Text)|| string.IsNullOrEmpty(toTimeTextBox.Text)) return false;

		if (fromDateTimePicker.Value > toDateTimePicker.Value) return false;

		if (int.Parse(fromTimeTextBox.Text) > int.Parse(toTimeTextBox.Text))
			if (fromDateTimePicker.Value == toDateTimePicker.Value)
				return false;

		return true;
	}
	#endregion

	[STAThread]
	private void summaryReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date");
			return;
		}

		LoadingScreen.ShowSplashScreen();
		ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox);
		LoadingScreen.CloseForm();
		showDataForm.ShowDialog();
	}

	[STAThread]
	private void detailReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date");
			return;
		}

		LoadingScreen.ShowSplashScreen();
		ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox, locationId, true);
		LoadingScreen.CloseForm();
		showDataForm.ShowDialog();
	}
}
