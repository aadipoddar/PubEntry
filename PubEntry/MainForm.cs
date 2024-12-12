using System;
using System.Drawing.Printing;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class MainForm : Form
{

	#region InitalLoading
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
		reservationComboBox.DataSource = (await CommonData.LoadTableData<ReservationTypeModel>("ReservationTypeTable")).ToList();
		reservationComboBox.DisplayMember = "Name";

		dateTimeLabel.Text = DateTime.Now.ToString();
	}

	private void ClearForm()
	{
		nameTextBox.Text = string.Empty;
		numberTextBox.Text = string.Empty;
		approvedByTextBox.Text = string.Empty;
		cashAmountTextBox.Text = "0";
		cardAmountTextBox.Text = "0";
		upiAmountTextBox.Text = "0";
		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";
		numberTextBox.ReadOnly = false;
		numberTextBox.Visible = true;
		nameTextBox.ReadOnly = false;
		numberTextBox.Focus();
		numberComboBox.Visible = false;
	}
	#endregion

	#region Validation
	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}
	#endregion

	#region Events
	private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

	private void numberTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		foundPerson = Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			numberTextBox.Text = foundPerson.Number;
			personFound = true;
			nameTextBox.ReadOnly = true;
			numberTextBox.ReadOnly = false;
		}

		else
		{
			personFound = false;
			nameTextBox.ReadOnly = false;
			nameTextBox.Text = string.Empty;
		}
	}

	private void nameTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		if (numberTextBox.Text != string.Empty && numberTextBox.ReadOnly == false)
			return;

		var foundPeople = Task.Run(async () => await PersonData.GetPersonByName(nameTextBox.Text)).Result;
		foundPerson = null;

		if (foundPeople.Count > 1)
		{
			numberComboBox.Visible = true;
			numberTextBox.Visible = false;

			numberComboBox.DataSource = foundPeople;
			numberComboBox.DisplayMember = "Number";
			numberComboBox.ValueMember = "Number";

			personFound = true;
		}

		else if (foundPeople.Count == 1)
		{
			foundPerson = foundPeople.FirstOrDefault();
			numberComboBox.Visible = false;
			numberTextBox.Visible = true;

			personFound = true;
		}

		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			numberTextBox.Text = foundPerson.Number;
			personFound = true;
			numberTextBox.ReadOnly = true;
			nameTextBox.ReadOnly = false;
		}

		else if (foundPerson == null && foundPeople.Count == 0)
		{
			personFound = false;
			numberComboBox.Visible = false;
			numberTextBox.Visible = true;
			numberTextBox.ReadOnly = false;
			numberTextBox.Text = string.Empty;
		}
	}

	private async void insertButton_Click(object sender, EventArgs e)
	{
		if (!personFound)
			await PersonData.InsertPersonTableData(nameTextBox.Text, numberTextBox.Text);

		if (numberComboBox.Visible == true)
		{
			var foundPersonNumber = numberComboBox.SelectedValue.ToString();
			foundPerson = Task.Run(async () => await PersonData.GetPersonByNumber(foundPersonNumber)).Result.FirstOrDefault();
		}

		else foundPerson = Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();

		transaction.PersonId = foundPerson.Id;
		transaction.Male = (int)Convert.ToInt64(maleTextBox.Text);
		transaction.Female = (int)Convert.ToInt64(femaleTextBox.Text);
		transaction.Cash = (int)Convert.ToInt64(cashAmountTextBox.Text);
		transaction.Card = (int)Convert.ToInt64(cardAmountTextBox.Text);
		transaction.UPI = (int)Convert.ToInt64(upiAmountTextBox.Text);
		transaction.Amex = (int)Convert.ToInt64(amexAmountTextBox.Text);
		transaction.ReservationType = reservationComboBox.SelectedIndex;
		transaction.DateTime = DateTime.Now;
		transaction.ApprovedBy = approvedByTextBox.Text == "" ? null : approvedByTextBox.Text;
		transaction.LocationId = locationId;
		transaction.EmployeeId = employeeId;

		await TransactionData.InsertTransactionTableData(transaction);

		PrintDialog printDialog = new();
		printDialog.Document = printDocumentCustomer;
		printDocumentCustomer.Print();

		printDialog.Document = printDocumentMerchant;
		printDocumentMerchant.Print();

		ClearForm();
	}

	private void DrawGraphics(PrintPageEventArgs e, string copyOf)
	{
		Graphics g = e.Graphics;
		Font font = new("Courier New", 15); // Use Courier New for better alignment on thermal printers

		StringFormat center = new(StringFormatFlags.FitBlackBox);
		center.Alignment = StringAlignment.Center;

		// Receipt Header
		int y = 0;
		g.DrawString($"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name} **", new Font("Courier New", 20, FontStyle.Bold), Brushes.Black, 10, y += 10);
		g.DrawString($"----- {copyOf} Copy -----", font, Brushes.Black, 10, y += 40);
		g.DrawString($"Slip No.: {Task.Run(async () => await TransactionData.GetTransactionIdbyDate(transaction.DateTime.ToString("yyyy-MM-dd HH:mm:ss"))).Result}", font, Brushes.Black, 10, y += 25);
		g.DrawString($"DT: {transaction.DateTime.ToString("dd/MM/yy HH:mm")}", font, Brushes.Black, 10, y += 25);
		g.DrawString($"Name: {foundPerson.Name}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Mobile Number: {foundPerson.Number}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Reservation Type: {Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Total Persons: {transaction.Male + transaction.Female}", font, Brushes.Black, 10, y += 20);
		g.DrawString("Male\tFemale", font, Brushes.Black, 10, y += 20);
		g.DrawString($"{transaction.Male}\t{transaction.Female}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Total Payment: {transaction.Cash + transaction.Card + transaction.UPI + transaction.Amex}", new("Courier New", 15, FontStyle.Bold), Brushes.Black, 10, y += 20);
		g.DrawString("Cash\tCard\tUPI\tAmex", font, Brushes.Black, 10, y += 20);
		g.DrawString($"{transaction.Cash}\t{transaction.Card}\t{transaction.UPI}\t{transaction.Amex}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		if (transaction.ApprovedBy != null) g.DrawString($"Approved By: {transaction.ApprovedBy}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Entered By: {Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name}", font, Brushes.Black, 10, y += 20);

		g.DrawString($"This coupon is non-transferable to any\nPerson or any other outlet. This coupon\nis to be redeemed until the end of the\noperations of the particular night:\n{transaction.DateTime.ToString("dd/MM/yy HH:mm")}\nThe hotel does not take liability or\nresponsibility if the coupon is lost\nby the guest", new Font("Courier New", 10), Brushes.Black, 10, y += 30);

		PaperSize ps58 = new("58mm Thermal", 220, y += 20);
		printDocumentCustomer.DefaultPageSettings.PaperSize = ps58;

		e.HasMorePages = false;
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e)
	{
		DrawGraphics(e, "Customer");
	}

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e)
	{
		DrawGraphics(e, "Merchant");
	}
	#endregion
}