using System;
using System.Drawing.Printing;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PubEntry;

public partial class MainForm : Form
{
	int employeeId, locationId;
	bool personFound = false;
	PersonModel foundPerson = new();
	TransactionModel transaction = new();

	public MainForm(int locationId, int employeeId)
	{
		InitializeComponent();

		this.employeeId = employeeId;
		this.locationId = locationId;

		Task task = LoadComboBox();
	}

	private async Task LoadComboBox()
	{
		reservationComboBox.DataSource = null;
		reservationComboBox.DataSource = (await DataAccess.LoadTableData<ReservationTypeModel>("ReservationTypeTable")).ToList();
		reservationComboBox.DisplayMember = "Name";

		dateTimeLabel.Text = DateTime.Now.ToString();
	}

	private void ClearForm()
	{
		nameTextBox.Text = string.Empty;
		numberTextBox.Text = string.Empty;
		cashAmountTextBox.Text = "0";
		cardAmountTextBox.Text = "0";
		upiAmountTextBox.Text = "0";
		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";
		approvedByTextBox.Text = string.Empty;
	}

	private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

	private void numberTextBox_TextChanged(object sender, EventArgs e)
	{
		foundPerson = Task.Run(async () => await DataAccess.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			numberTextBox.Text = foundPerson.Number;
			personFound = true;
			nameTextBox.ReadOnly = true;
		}

		else
		{
			personFound = false;
			nameTextBox.ReadOnly = false;
			nameTextBox.Text = string.Empty;
		}
	}

	private async void insertButton_Click(object sender, EventArgs e)
	{
		if (!personFound)
			await DataAccess.InsertPersonTableData(nameTextBox.Text, numberTextBox.Text);

		foundPerson = Task.Run(async () => await DataAccess.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();

		transaction.PersonId = foundPerson.Id;
		transaction.Male = (int)Convert.ToInt64(maleTextBox.Text);
		transaction.Female = (int)Convert.ToInt64(femaleTextBox.Text);
		transaction.Cash = (int)Convert.ToInt64(cashAmountTextBox.Text);
		transaction.Card = (int)Convert.ToInt64(cardAmountTextBox.Text);
		transaction.UPI = (int)Convert.ToInt64(upiAmountTextBox.Text);
		transaction.ReservationType = reservationComboBox.SelectedIndex;
		transaction.DateTime = DateTime.Now;
		transaction.ApprovedBy = approvedByTextBox.Text == "" ? null : approvedByTextBox.Text;
		transaction.LocationId = locationId;
		transaction.EmployeeId = employeeId;

		await DataAccess.InsertTransactionTableData(transaction);

		PrintDialog printDialog = new PrintDialog();
		printDialog.Document = printDocument;

		if (printDialog.ShowDialog() == DialogResult.OK)
			printDocument.Print();

		ClearForm();
	}

	private decimal CalculateSubtotal(decimal[] prices, int[] quantities)
	{
		decimal subtotal = 0;
		for (int i = 0; i < prices.Length; i++)
		{
			subtotal += prices[i] * quantities[i];
		}
		return subtotal;
	}

	private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics g = e.Graphics;
		Font font = new Font("Courier New", 9); // Use Courier New for better alignment on thermal printers

		// Receipt Header
		g.DrawString($"** {Task.Run(async () => await DataAccess.GetLocationNameById(locationId)).Result} **", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 10, 10);
		//g.DrawString($"Serial Number: {Task.Run(async () => await DataAccess.GetTransactionIdbyDate(transaction.DateTime.ToString())).Result}", font, Brushes.Black, 10, 60);
		g.DrawString($"Name: {foundPerson.Name}", font, Brushes.Black, 10, 30);
		g.DrawString($"Mobile Number: {foundPerson.Number}", font, Brushes.Black, 10, 45);
		g.DrawString($"Reservation Type: {Task.Run(async () => await DataAccess.GetReservationTypeById(transaction.ReservationType)).Result}", font, Brushes.Black, 10, 60);

		g.DrawString("---------------------------------", font, Brushes.Black, 10, 75);
		g.DrawString($"Total Persons: {transaction.Male + transaction.Female}", font, Brushes.Black, 10, 90);
		g.DrawString("Male\tFemale", font, Brushes.Black, 10, 105);
		g.DrawString($"{transaction.Male}\t{transaction.Female}", font, Brushes.Black, 10, 120);

		g.DrawString("---------------------------------", font, Brushes.Black, 10, 140);
		g.DrawString($"Total Payment: {transaction.Cash + transaction.Card + transaction.UPI}", font, Brushes.Black, 10, 155);
		g.DrawString("Cash\tCard\tUPI", font, Brushes.Black, 10, 170);
		g.DrawString($"{transaction.Cash}\t{transaction.Card}\t{transaction.UPI}", font, Brushes.Black, 10, 185);

		g.DrawString("---------------------------------", font, Brushes.Black, 10, 205);
		g.DrawString($"Approved By: {transaction.ApprovedBy}", font, Brushes.Black, 10, 225);

		g.DrawString($"This coupon is non-transferable to\nany Person or any other outlet\nThis coupon is to be redeemed until\nthe end of the operations of the\nparticular night:\n{transaction.DateTime.ToString()}\nThe hotel does not take liability\nor responsibility if the coupon is\nlost by the guest", font, Brushes.Black, 10, 265);

		e.HasMorePages = false;
	}
}
