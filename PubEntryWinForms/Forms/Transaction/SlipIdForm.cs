﻿using System.Reflection;

using PubEntry.Forms.Transaction;

namespace PubEntry.Forms.Admin;

public partial class SlipIdForm : Form
{
	public SlipIdForm() => InitializeComponent();

	private void SlipIdForm_Load(object sender, EventArgs e) =>
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

	#region Validation

	private void slipIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(slipIdTextBox.Text);

	#endregion

	private async void goButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Slip Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		TransactionModel transaction = await CommonData.LoadTableDataById<TransactionModel>(TableNames.Transaction, int.Parse(slipIdTextBox.Text));

		if (transaction is null)
		{
			MessageBox.Show("Invalid Slip Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		UpdateTransactionForm updateTransactionForm = new(transaction);
		updateTransactionForm.ShowDialog();
	}
}
