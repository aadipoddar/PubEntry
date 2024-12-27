namespace PubEntry.Admin;

public partial class TransactionForm : Form
{
	public TransactionForm()
	{
		InitializeComponent();
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		TransactionForm transactionForm = new();
		transactionForm.ShowDialog();
	}
}
