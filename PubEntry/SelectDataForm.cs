namespace PubEntry;

public partial class SelectDataForm : Form
{
	public SelectDataForm()
	{
		InitializeComponent();

		LoadComboBox();
	}

	private void LoadComboBox()
	{
		if (DateTime.Now.Hour > 4)
			fromTimeTextBox.Text = (DateTime.Now.Hour - 3).ToString();

		else fromTimeTextBox.Text = DateTime.Now.Hour.ToString();

		toTimeTextBox.Text = DateTime.Now.Hour.ToString();
	}

	#region Validation
	private bool ValidateTime()
	{
		if (Convert.ToInt64(toTimeTextBox.Text) >= Convert.ToInt64(fromTimeTextBox.Text))
			return true;

		else return false;
	}

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
		if (ValidateTime())
		{
			ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox);
			showDataForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Time");
	}

	private void detailReportButton_Click(object sender, EventArgs e)
	{
		if (ValidateTime())
		{
			ShowDataForm showDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox, true);
			showDataForm.ShowDialog();
		}

		else MessageBox.Show("Incorrect Time");
	}
}
