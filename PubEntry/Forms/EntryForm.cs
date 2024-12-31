using System.Drawing.Printing;
using System.Reflection;

using Timer = System.Windows.Forms.Timer;

namespace PubEntry;

public partial class EntryForm : Form
{
	#region InitalLoading
	private Timer inactivityTimer;
	private const int InactivityLimit = 5 * 60 * 1000;

	private int transactionId, userId, locationId;
	AdvanceModel advanceModel;

	public EntryForm(int locationId, int userId)
	{
		InitializeComponent();

		this.userId = userId;
		this.locationId = locationId;
	}

	public EntryForm(TransactionModel updateTransactionModel)
	{
		InitializeComponent();
	}

	private async void EntryForm_Load(object sender, EventArgs e)
	{
		await LoadComboBox();
		InitializeInactivityTimer();
		SubscribeToTextChangedEvents();
	}

	private async Task LoadComboBox()
	{
		reservationComboBox.DataSource = await CommonData.LoadTableData<ReservationTypeModel>("ReservationTypeTable");
		reservationComboBox.DisplayMember = nameof(ReservationTypeModel.Name);
		reservationComboBox.ValueMember = nameof(ReservationTypeModel.Id);

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

		paymentModeComboBox.DataSource = await CommonData.LoadTableData<PaymentModeModel>("PaymentModeTable");
		paymentModeComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentModeComboBox.ValueMember = nameof(PaymentModeModel.Id);


		amountDataGridView.DefaultCellStyle.Font = new Font("Arial", 20);
	}

	private void ClearForm()
	{
		nameTextBox.Text = string.Empty;
		numberTextBox.Text = string.Empty;
		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";
		amountTextBox.Text = "0";
		amountDataGridView.Rows.Clear();
		advancePanel.Visible = false;
		nameTextBox.ReadOnly = false;
		loyaltyCheckBox.Checked = false;
		approvedByTextBox.Text = string.Empty;
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
		if (string.IsNullOrWhiteSpace(numberTextBox.Text)) return false;
		if (string.IsNullOrWhiteSpace(nameTextBox.Text)) return false;
		if (string.IsNullOrWhiteSpace(maleTextBox.Text)) maleTextBox.Text = "0";
		if (string.IsNullOrWhiteSpace(femaleTextBox.Text)) femaleTextBox.Text = "0";

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

			advanceModel = await TransactionData.LoadAdvanceByDateLocationPerson(locationId, foundPerson.Id);
			if (advanceModel != null)
			{
				advancePanel.Visible = true;
				advanceAmountTextBox.Text = (await TransactionData.GetTotalAdvanceAmountById(advanceModel.Id)).ToString();
				bookingAmountTextBox.Text = advanceModel.BookingAmount.ToString();
			}

			else advancePanel.Visible = false;
		}

		else
		{
			nameTextBox.Text = string.Empty;
			nameTextBox.ReadOnly = false;
			loyaltyCheckBox.Checked = false;
			advancePanel.Visible = false;
		}
	}

	private void addButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(amountTextBox.Text))
		{
			MessageBox.Show("Please enter Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		amountDataGridView.Rows.Add((paymentModeComboBox.SelectedItem as PaymentModeModel).Id, (paymentModeComboBox.SelectedItem as PaymentModeModel).Name, amountTextBox.Text);
		amountTextBox.Text = "0";
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
			Id = 0,
			LocationId = locationId,
			UserId = userId,
			DateTime = DateTime.Now,
			PersonId = personModel.Id,
			Male = (int)Convert.ToInt64(maleTextBox.Text),
			Female = (int)Convert.ToInt64(femaleTextBox.Text),
			ReservationType = (reservationComboBox.SelectedItem as ReservationTypeModel).Id,
			ApprovedBy = approvedByTextBox.Text
		};

		transactionId = await TransactionData.TransactionInsert(transactionModel);

		foreach (DataGridViewRow row in amountDataGridView.Rows)
		{
			await PaymentData.PaymentInsert(new PaymentModel
			{
				Id = 0,
				TransactionId = transactionId,
				AdvanceId = 0,
				PaymentModeId = int.Parse(row.Cells[0].Value.ToString()),
				Amount = int.Parse(row.Cells[2].Value.ToString())
			});
		}

		if (advanceAmountTextBox.Visible)
			await TransactionData.ClearAdvance(advanceModel.Id, transactionId);

		PrintDialog printDialog = new();
		printDialog.Document = printDocumentCustomer;
		printDocumentCustomer.Print();

		printDialog = new();
		printDialog.Document = printDocumentMerchant;
		printDocumentMerchant.Print();

		ClearForm();
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e)
		=> PrintReceipt.DrawGraphics(e, "Customer", transactionId, advanceModel != null ? advanceModel.Id : 0);

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e)
		=> PrintReceipt.DrawGraphics(e, "Merchant", transactionId, advanceModel != null ? advanceModel.Id : 0);
	#endregion
}
