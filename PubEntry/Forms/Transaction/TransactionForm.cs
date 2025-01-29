using System.Drawing.Printing;
using System.Reflection;

using Timer = System.Windows.Forms.Timer;

namespace PubEntry.Forms.Transaction;

public partial class TransactionForm : Form
{
	#region InactivityTimer

	private Timer inactivityTimer;

	private async void InitializeInactivityTimer()
	{
		int InactivityLimit = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.InactivityTime)) * 60 * 1000;
		inactivityTimer = new Timer { Interval = InactivityLimit };
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
		numberTextBox.TextChanged += ResetInactivityTimer;
		nameTextBox.TextChanged += ResetInactivityTimer;
		loyaltyCheckBox.CheckedChanged += ResetInactivityTimer;
		maleTextBox.TextChanged += ResetInactivityTimer;
		femaleTextBox.TextChanged += ResetInactivityTimer;
		cashTextBox.TextChanged += ResetInactivityTimer;
		cardTextBox.TextChanged += ResetInactivityTimer;
		upiTextBox.TextChanged += ResetInactivityTimer;
		amexTextBox.TextChanged += ResetInactivityTimer;
		approvedByTextBox.TextChanged += ResetInactivityTimer;
		reservationComboBox.SelectedIndexChanged += ResetInactivityTimer;
		saveButton.Click += ResetInactivityTimer;
	}

	#endregion

	#region InitalLoading

	private readonly int _userId, _locationId;
	private int transactionId, foundAdvanceId;

	public TransactionForm(int locationId, int userId)
	{
		InitializeComponent();

		_userId = userId;
		_locationId = locationId;
	}

	private async void EntryForm_Load(object sender, EventArgs e)
	{
		await LoadData();
		InitializeInactivityTimer();
		SubscribeToTextChangedEvents();
	}

	private async Task LoadData()
	{
		reservationComboBox.DataSource = await CommonData.LoadTableData<ReservationTypeModel>(Table.ReservationType);
		reservationComboBox.DisplayMember = nameof(ReservationTypeModel.Name);
		reservationComboBox.ValueMember = nameof(ReservationTypeModel.Id);

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	#endregion

	#region Validation

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private void textBox_Click(object sender, EventArgs e) => (sender as TextBox).SelectAll();

	private void dateChangeTimer_Tick(object sender, EventArgs e) => dateTimeLabel.Text = DateTime.Now.ToString();

	private bool ValidateFields()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;
		if (string.IsNullOrEmpty(maleTextBox.Text)) maleTextBox.Text = "0";
		if (string.IsNullOrEmpty(femaleTextBox.Text)) maleTextBox.Text = "0";
		if (string.IsNullOrEmpty(cashTextBox.Text)) cashTextBox.Text = "0";
		if (string.IsNullOrEmpty(cardTextBox.Text)) cardTextBox.Text = "0";
		if (string.IsNullOrEmpty(upiTextBox.Text)) upiTextBox.Text = "0";
		if (string.IsNullOrEmpty(amexTextBox.Text)) amexTextBox.Text = "0";

		return true;
	}

	#endregion

	#region LoadAdvanceData

	private async void NumberTextBox_TextChanged(object sender, EventArgs e)
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson is not null)
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

		await LoadPersonAdvance();
	}

	private async Task LoadPersonAdvance()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson is not null)
		{
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(_locationId, foundPerson.Id);
			if (foundAdvance is not null)
			{
				foundAdvanceId = foundAdvance.Id;
				advancePanel.Visible = true;
				approvedByTextBox.Text = foundAdvance.ApprovedBy;
				bookingTextBox.Text = foundAdvance.Booking.ToString();
				var advanceDetail = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
				advanceTextBox.Text = advanceDetail.Sum(x => x?.Amount).ToString();

				return;
			}
		}

		foundAdvanceId = 0;
		advancePanel.Visible = false;
		approvedByTextBox.Text = string.Empty;
	}

	#endregion

	#region Saving

	private async void SaveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateFields())
		{
			MessageBox.Show("Please all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		await InsertTransaction();

		if (foundAdvanceId is not 0) await AdvanceData.ClearAdvance(foundAdvanceId, transactionId);

		PrintTransactionThermal();

		ClearForm();
	}

	private async Task InsertTransaction()
	{
		PersonModel personModel = new()
		{
			Id = 0,
			Name = nameTextBox.Text,
			Number = numberTextBox.Text,
			Loyalty = loyaltyCheckBox.Checked
		};

		if (nameTextBox.ReadOnly == false) personModel.Id = await PersonData.InsertPerson(personModel);
		personModel.Id = await PersonData.UpdatePerson(personModel);

		transactionId = await TransactionData.InsertTransaction(new TransactionModel
		{
			Id = 0,
			PersonId = personModel.Id,
			Male = int.Parse(maleTextBox.Text),
			Female = int.Parse(femaleTextBox.Text),
			Cash = int.Parse(cashTextBox.Text),
			Card = int.Parse(cardTextBox.Text),
			UPI = int.Parse(upiTextBox.Text),
			Amex = int.Parse(amexTextBox.Text),
			ReservationTypeId = (reservationComboBox.SelectedItem as ReservationTypeModel).Id,
			DateTime = DateTime.Now,
			ApprovedBy = approvedByTextBox.Text,
			LocationId = _locationId,
			UserId = _userId
		});
	}

	private void ClearForm()
	{
		numberTextBox.Text = string.Empty;
		nameTextBox.Text = string.Empty;
		nameTextBox.ReadOnly = false;
		numberTextBox.Focus();
		loyaltyCheckBox.Checked = false;

		approvedByTextBox.Text = string.Empty;
		reservationComboBox.SelectedIndex = 0;

		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";

		cashTextBox.Text = "0";
		cardTextBox.Text = "0";
		upiTextBox.Text = "0";
		amexTextBox.Text = "0";
	}

	#endregion

	#region Printing
	private void PrintTransactionThermal()
	{
		printDocumentCustomer.Print();
		printDocumentMerchant.Print();
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Customer", transactionId, foundAdvanceId);

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Merchant", transactionId, foundAdvanceId);
	#endregion
}
