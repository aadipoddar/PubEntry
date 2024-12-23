using System.Drawing.Printing;
using System.Reflection;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class MainForm : Form
{
	#region InitalLoading
	int employeeId, locationId;
	bool personFound = false;
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
		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
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
		loyaltyCheckBox.Checked = false;
		numberTextBox.Focus();
	}
	#endregion

	#region Validation
	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private bool ValidateFields()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;
		if (string.IsNullOrEmpty(maleTextBox.Text)) maleTextBox.Text = "0";
		if (string.IsNullOrEmpty(femaleTextBox.Text)) femaleTextBox.Text = "0";
		if (string.IsNullOrEmpty(cashAmountTextBox.Text)) cashAmountTextBox.Text = "0";
		if (string.IsNullOrEmpty(cardAmountTextBox.Text)) cardAmountTextBox.Text = "0";
		if (string.IsNullOrEmpty(upiAmountTextBox.Text)) upiAmountTextBox.Text = "0";
		if (string.IsNullOrEmpty(amexAmountTextBox.Text)) amexAmountTextBox.Text = "0";

		return true;
	}
	#endregion

	#region Events
	private void dateChangeTimer_Tick(object sender, EventArgs e) => dateTimeLabel.Text = DateTime.Now.ToString();

	private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

	private void numberTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		var foundPerson = Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault();
		if (foundPerson != null)
		{
			personFound = true;
			nameTextBox.Text = foundPerson.Name;
			nameTextBox.ReadOnly = true;
			if (foundPerson.Loyalty == 1) loyaltyCheckBox.Checked = true;
			else loyaltyCheckBox.Checked = false;
		}

		else
		{
			personFound = false;
			nameTextBox.Text = string.Empty;
			nameTextBox.ReadOnly = false;
			loyaltyCheckBox.Checked = false;
		}
	}

	private async void insertButton_Click(object sender, EventArgs e)
	{
		if (!ValidateFields())
		{
			MessageBox.Show("Enter all Fields");
			return;
		}

		if (!personFound)
			await PersonData.InsertPersonTableData(nameTextBox.Text, numberTextBox.Text, loyaltyCheckBox.Checked ? 1 : 0);

		await PersonData.UpdatePersonTableData(numberTextBox.Text, loyaltyCheckBox.Checked ? 1 : 0);

		transaction.PersonId = Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault().Id;
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

		printDialog = new();
		printDialog.Document = printDocumentMerchant;
		printDocumentMerchant.Print();

		ClearForm();
	}
	#endregion

	#region Printing
	string MakeStringHeaderAmount(TransactionModel transaction)
	{
		string finalString = "";

		if (transaction.Cash > 0)
			finalString += "Cash\t";

		if (transaction.Card > 0)
			finalString += "Card\t";

		if (transaction.UPI > 0)
			finalString += "UPI\t";

		if (transaction.Amex > 0)
			finalString += "Amex\t";

		return finalString;
	}

	string MakeStringAmount(TransactionModel transaction)
	{
		string finalString = "";

		if (transaction.Cash > 0)
			finalString += $"{transaction.Cash}\t";

		if (transaction.Card > 0)
			finalString += $"{transaction.Card}\t";

		if (transaction.UPI > 0)
			finalString += $"{transaction.UPI}\t";

		if (transaction.Amex > 0)
			finalString += $"{transaction.Amex}\t";

		return finalString;
	}

	private void DrawGraphics(PrintPageEventArgs e, string copyOf)
	{
		Graphics g = e.Graphics;
		Font font = new("Courier New", 12, FontStyle.Bold);

		StringFormat center = new(StringFormatFlags.FitBlackBox);
		center.Alignment = StringAlignment.Center;

		int y = 0;
		g.DrawString($"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name} **", new Font("Courier New", 20, FontStyle.Bold), Brushes.Black, 25, y += 10);
		g.DrawString($"----- {copyOf} Copy -----", font, Brushes.Black, 10, y += 40);
		g.DrawString($"Slip No.: {Task.Run(async () => await TransactionData.GetTransactionIdbyDateAndPersonId(transaction.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), transaction.PersonId)).Result}", font, Brushes.Black, 10, y += 25);
		g.DrawString($"DT: {transaction.DateTime.ToString("dd/MM/yy HH:mm")}", font, Brushes.Black, 10, y += 25);
		g.DrawString($"Name: {Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBox.Text)).Result.FirstOrDefault().Name}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Contact: {numberTextBox.Text}", font, Brushes.Black, 10, y += 20);
		if (loyaltyCheckBox.Checked) g.DrawString("Loyalty Member", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Reservation: {Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Total Persons: {transaction.Male + transaction.Female}", font, Brushes.Black, 10, y += 20);
		g.DrawString("Male\tFemale", font, Brushes.Black, 10, y += 20);
		g.DrawString($"{transaction.Male}\t{transaction.Female}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Total: {transaction.Cash + transaction.Card + transaction.UPI + transaction.Amex}", new("Courier New", 15, FontStyle.Bold), Brushes.Black, 10, y += 20);
		g.DrawString($"{MakeStringHeaderAmount(transaction)}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"{MakeStringAmount(transaction)}", font, Brushes.Black, 10, y += 20);

		g.DrawString("--------------------------", font, Brushes.Black, 10, y += 20);
		if (transaction.ApprovedBy != null) g.DrawString($"Approved By: {transaction.ApprovedBy}", font, Brushes.Black, 10, y += 20);
		g.DrawString($"Entered By: {Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name}", font, Brushes.Black, 10, y += 20);

		g.DrawString("This coupon is non-transferable to any", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 30);
		g.DrawString("Person or any other outlet. This coupon", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString("is to be redeemed until the end of the", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString("operations of the particular night:", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString($"{transaction.DateTime.ToString("dd/MM/yy HH:mm")}", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString("The hotel does not take liability or", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString("responsibility if the coupon is lost", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);
		g.DrawString("by the guest", new Font("Courier New", 8, FontStyle.Bold), Brushes.Black, 8, y += 10);

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