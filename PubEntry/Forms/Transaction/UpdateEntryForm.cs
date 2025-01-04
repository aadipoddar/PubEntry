using System.Drawing.Printing;
using System.Reflection;

using Timer = System.Windows.Forms.Timer;

namespace PubEntry.Forms.Transaction;

public partial class UpdateEntryForm : Form
{
	#region InitalLoading
	private Timer inactivityTimer;
	private const int InactivityLimit = 5 * 60 * 1000;

	private int transactionId, userId, locationId;
	TransactionModel updateTransactionModel;

	public UpdateEntryForm(TransactionModel updateTransactionModel)
	{
		InitializeComponent();

		this.updateTransactionModel = updateTransactionModel;
	}

	private async void UpdateEntryForm_Load(object sender, EventArgs e)
	{
		await LoadComboBox(updateTransactionModel);
		InitializeInactivityTimer();
		SubscribeToTextChangedEvents();
	}

	private async Task LoadComboBox(TransactionModel updateTransactionModel)
	{
		reservationComboBox.DataSource = await CommonData.LoadTableData<ReservationTypeModel>("ReservationTypeTable");
		reservationComboBox.DisplayMember = nameof(ReservationTypeModel.Name);
		reservationComboBox.ValueMember = nameof(ReservationTypeModel.Id);

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

		PersonModel personModel = (await CommonData.LoadTableDataById<PersonModel>("PersonTable", updateTransactionModel.PersonId)).FirstOrDefault();
		if (personModel != null)
		{
			numberTextBox.Text = personModel.Number;
			nameTextBox.Text = personModel.Name;
			loyaltyCheckBox.Checked = personModel.Loyalty;
			maleTextBox.Text = updateTransactionModel.Male.ToString();
			femaleTextBox.Text = updateTransactionModel.Female.ToString();
			cashAmountTextBox.Text = updateTransactionModel.Cash.ToString();
			cardAmountTextBox.Text = updateTransactionModel.Card.ToString();
			upiAmountTextBox.Text = updateTransactionModel.UPI.ToString();
			amexAmountTextBox.Text = updateTransactionModel.Amex.ToString();
			reservationComboBox.SelectedIndex = updateTransactionModel.ReservationType;
			approvedByTextBox.Text = updateTransactionModel.ApprovedBy;
			userId = updateTransactionModel.UserId;
			locationId = updateTransactionModel.LocationId;
		}
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
		loyaltyCheckBox.CheckedChanged += ResetInactivityTimer;
		maleTextBox.TextChanged += ResetInactivityTimer;
		femaleTextBox.TextChanged += ResetInactivityTimer;
		cashAmountTextBox.TextChanged += ResetInactivityTimer;
		cardAmountTextBox.TextChanged += ResetInactivityTimer;
		upiAmountTextBox.TextChanged += ResetInactivityTimer;
		amexAmountTextBox.TextChanged += ResetInactivityTimer;
		approvedByTextBox.TextChanged += ResetInactivityTimer;
		reservationComboBox.SelectedIndexChanged += ResetInactivityTimer;
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

	private async void numberTextBox_TextChanged(object sender, EventArgs e)
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			nameTextBox.ReadOnly = true;
			loyaltyCheckBox.Checked = foundPerson.Loyalty;
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
			MessageBox.Show("Please all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		PersonModel personModel = new()
		{
			Id = 0,
			Name = nameTextBox.Text,
			Number = numberTextBox.Text,
			Loyalty = loyaltyCheckBox.Checked
		};

		if (nameTextBox.ReadOnly == false) personModel.Id = await PersonData.PersonInsert(personModel);
		personModel.Id = await PersonData.PersonUpdate(personModel);

		TransactionModel transactionModel = new()
		{
			Id = updateTransactionModel == null ? 0 : updateTransactionModel.Id,
			PersonId = personModel.Id,
			Male = (int)Convert.ToInt64(maleTextBox.Text),
			Female = (int)Convert.ToInt64(femaleTextBox.Text),
			Cash = (int)Convert.ToInt64(cashAmountTextBox.Text),
			Card = (int)Convert.ToInt64(cardAmountTextBox.Text),
			UPI = (int)Convert.ToInt64(upiAmountTextBox.Text),
			Amex = (int)Convert.ToInt64(amexAmountTextBox.Text),
			ReservationType = (reservationComboBox.SelectedItem as ReservationTypeModel).Id,
			DateTime = DateTime.Now,
			ApprovedBy = approvedByTextBox.Text,
			LocationId = locationId,
			UserId = userId
		};

		transactionId = await TransactionData.TransactionUpdate(transactionModel);

		PrintDialog printDialog = new();
		printDialog.Document = printDocumentCustomer;
		printDocumentCustomer.Print();

		printDialog = new();
		printDialog.Document = printDocumentMerchant;
		printDocumentMerchant.Print();

		ClearForm();
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Customer", transactionId);

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Merchant", transactionId);
	#endregion
}
