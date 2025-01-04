using PubEntry.Forms.Transaction;

namespace PubEntry.Forms.Admin;

public partial class SlipIdForm : Form
{
	public SlipIdForm() => InitializeComponent();

	private void slipIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(slipIdTextBox.Text)) return false;
		return true;
	}

	private async void goButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Slip Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		TransactionModel transaction = (await CommonData.LoadTableDataById<TransactionModel>("TransactionTable", int.Parse(slipIdTextBox.Text))).FirstOrDefault();

		if (transaction == null)
		{
			MessageBox.Show("Invalid Slip Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		UpdateEntryForm updateEntryForm = new(transaction);
		updateEntryForm.ShowDialog();
	}
}
