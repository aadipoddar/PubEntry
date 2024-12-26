using System.Drawing.Printing;
using System.Reflection;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using Timer = System.Windows.Forms.Timer;

namespace PubEntry;

public partial class EntryForm : Form
{
	private Timer inactivityTimer;
	private const int InactivityLimit = 5 * 60 * 1000;

	#region InitalLoading
	int transactionId, employeeId, locationId;

	public EntryForm(int locationId, int employeeId)
	{
		InitializeComponent();

		this.employeeId = employeeId;
		this.locationId = locationId;

		Task task = LoadComboBox();

		InitializeInactivityTimer();
		SubscribeToTextChangedEvents();
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
		amexAmountTextBox.Text = "0";
		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";
		numberTextBox.ReadOnly = false;
		numberTextBox.Visible = true;
		nameTextBox.ReadOnly = false;
		loyaltyCheckBox.Checked = false;
		numberTextBox.Focus();
	}
	#endregion

	#region InactivityTimer
	private void InitializeInactivityTimer()
	{
		inactivityTimer = new Timer();
		inactivityTimer.Interval = InactivityLimit;
		inactivityTimer.Tick += InactivityTimer_Tick;
		inactivityTimer.Start();
	}

	private void InactivityTimer_Tick(object sender, EventArgs e) => Close();

	private void ResetInactivityTimer(object sender, EventArgs e)
	{
		inactivityTimer.Stop();
		inactivityTimer.Start();
	}

	private void SubscribeToTextChangedEvents()
	{
		nameTextBox.TextChanged += ResetInactivityTimer;
		numberTextBox.TextChanged += ResetInactivityTimer;
		maleTextBox.TextChanged += ResetInactivityTimer;
		femaleTextBox.TextChanged += ResetInactivityTimer;
		cashAmountTextBox.TextChanged += ResetInactivityTimer;
		cardAmountTextBox.TextChanged += ResetInactivityTimer;
		upiAmountTextBox.TextChanged += ResetInactivityTimer;
		amexAmountTextBox.TextChanged += ResetInactivityTimer;
		approvedByTextBox.TextChanged += ResetInactivityTimer;
		saveButton.Click += ResetInactivityTimer;
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

	private async void numberTextBox_KeyUp(object sender, KeyEventArgs e)
	{
		var foundPerson = await PersonData.GetPersonByNumber(numberTextBox.Text);
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			nameTextBox.ReadOnly = true;
			if (foundPerson.Loyalty == 1) loyaltyCheckBox.Checked = true;
			else loyaltyCheckBox.Checked = false;
		}

		else
		{
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

		PersonModel personModel = new();
		personModel.Id = 0;
		personModel.Name = nameTextBox.Text;
		personModel.Number = numberTextBox.Text;
		personModel.Loyalty = loyaltyCheckBox.Checked ? 1 : 0;

		if (nameTextBox.ReadOnly == false)
			personModel.Id = await PersonData.InsertPerson(personModel);

		personModel.Id = await PersonData.UpdatePerson(personModel);

		TransactionModel transactionModel = new();
		transactionModel.Id = 0;
		transactionModel.PersonId = personModel.Id;
		transactionModel.Male = (int)Convert.ToInt64(maleTextBox.Text);
		transactionModel.Female = (int)Convert.ToInt64(femaleTextBox.Text);
		transactionModel.Cash = (int)Convert.ToInt64(cashAmountTextBox.Text);
		transactionModel.Card = (int)Convert.ToInt64(cardAmountTextBox.Text);
		transactionModel.UPI = (int)Convert.ToInt64(upiAmountTextBox.Text);
		transactionModel.Amex = (int)Convert.ToInt64(amexAmountTextBox.Text);
		transactionModel.ReservationType = reservationComboBox.SelectedIndex;
		transactionModel.DateTime = DateTime.Now;
		transactionModel.ApprovedBy = approvedByTextBox.Text == "" ? null : approvedByTextBox.Text;
		transactionModel.LocationId = locationId;
		transactionModel.EmployeeId = employeeId;

		transactionId = Task.Run(async () => await TransactionData.InsertTransaction(transactionModel)).Result;

		PrintDialog printDialog = new();
		printDialog.Document = printDocumentCustomer;
		printDocumentCustomer.Print();

		printDialog = new();
		printDialog.Document = printDocumentMerchant;
		printDocumentMerchant.Print();

		ClearForm();
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e) =>
		PrintReceipt.DrawGraphics(e, "Customer", transactionId);

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e) =>
		PrintReceipt.DrawGraphics(e, "Merchant", transactionId);
	#endregion
}
