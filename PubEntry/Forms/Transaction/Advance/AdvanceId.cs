using System.Reflection;

namespace PubEntry.Forms.Transaction.Advance;

public partial class AdvanceId : Form
{
	public AdvanceId() => InitializeComponent();

	private void AdvanceId_Load(object sender, EventArgs e) =>
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

	#region Validation

	private void advanceIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(advanceIdTextBox.Text);

	#endregion

	private async void goButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Advance Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		var advance = await CommonData.LoadTableDataById<AdvanceModel>(TableNames.Advance, int.Parse(advanceIdTextBox.Text));

		if (advance is null || advance.TransactionId != 0)
		{
			MessageBox.Show("Invalid Advance Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		UpdateAdvanceForm updateAdvance = new(advance);
		updateAdvance.ShowDialog();
	}
}