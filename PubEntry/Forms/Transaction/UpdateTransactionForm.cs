using System.Drawing.Printing;
using System.Reflection;

namespace PubEntry.Forms.Transaction;

public partial class UpdateTransactionForm : Form
{
	private readonly TransactionModel _transaction;
	private int _foundAdvanceId;

	public UpdateTransactionForm(TransactionModel transaction)
	{
		InitializeComponent();

		_transaction = transaction;
	}

	#region LoadData

	private async void EntryForm_Load(object sender, EventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		reservationComboBox.DataSource = await CommonData.LoadTableDataByStatus<ReservationTypeModel>(TableNames.ReservationType);
		reservationComboBox.DisplayMember = nameof(ReservationTypeModel.Name);
		reservationComboBox.ValueMember = nameof(ReservationTypeModel.Id);

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

		if (_transaction is not null)
		{
			var person = await CommonData.LoadTableDataById<PersonModel>(TableNames.Person, _transaction.PersonId);
			if (person is not null)
			{
				numberTextBox.Text = person.Number;
				maleTextBox.Text = _transaction.Male.ToString();
				femaleTextBox.Text = _transaction.Female.ToString();
				cashTextBox.Text = _transaction.Cash.ToString();
				cardTextBox.Text = _transaction.Card.ToString();
				upiTextBox.Text = _transaction.UPI.ToString();
				amexTextBox.Text = _transaction.Amex.ToString();
				dateTimeLabel.Text = _transaction.DateTime.ToString();
				reservationComboBox.SelectedValue = _transaction.ReservationTypeId;
				approvedByTextBox.Text = _transaction.ApprovedBy.ToString();
				locationComboBox.SelectedValue = _transaction.LocationId;
			}
			else MessageBox.Show("Something is Seriously Wrong. Contact Developer", "Cannot Load Data");
		}
		else MessageBox.Show("Something is Seriously Wrong. Contact Developer", "Cannot Load Data");
	}

	private async void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) => await LoadPersonAdvance();

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
			AdvanceModel foundAdvance;

			if (foundPerson.Id == _transaction.PersonId && (locationComboBox.SelectedItem as LocationModel).Id == _transaction.LocationId)
			{
				foundAdvance = await AdvanceData.LoadAdvanceByTransactionId(_transaction.Id);
				if (foundAdvance is not null)
				{
					_foundAdvanceId = foundAdvance.Id;
					advancePanel.Visible = true;
					bookingTextBox.Text = foundAdvance.Booking.ToString();
					var advanceDetail = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
					advanceTextBox.Text = advanceDetail.Sum(x => x.Amount).ToString();
					return;
				}
			}

			else
			{
				foundAdvance = _transaction.DateTime.TimeOfDay < TimeSpan.FromHours(17) ?
					await AdvanceData.LoadAdvanceByDateLocationPerson((locationComboBox.SelectedItem as LocationModel).Id, foundPerson.Id, _transaction.DateTime.AddDays(-1))
					: await AdvanceData.LoadAdvanceByDateLocationPerson((locationComboBox.SelectedItem as LocationModel).Id, foundPerson.Id, _transaction.DateTime);
				if (foundAdvance is not null)
				{
					_foundAdvanceId = foundAdvance.Id;
					advancePanel.Visible = true;
					approvedByTextBox.Text = foundAdvance.ApprovedBy;
					bookingTextBox.Text = foundAdvance.Booking.ToString();
					var advanceDetail = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
					advanceTextBox.Text = advanceDetail.Sum(x => x.Amount).ToString();
					return;
				}
			}
		}

		_foundAdvanceId = 0;
		advancePanel.Visible = false;
		approvedByTextBox.Text = _transaction.ApprovedBy;
	}

	#endregion

	#region Validation
	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private void textBox_Click(object sender, EventArgs e) => (sender as TextBox).SelectAll();

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

	#region Saving
	private async void SaveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateFields())
		{
			MessageBox.Show("Please all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		await UpdateTransaction();
		await UpdateAdvance();

		PrintTransactionThermal();

		Close();
	}

	private async Task UpdateAdvance()
	{
		var existingAdvance = await AdvanceData.LoadAdvanceByTransactionId(_transaction.Id);
		if (existingAdvance is not null) await AdvanceData.ClearAdvance(existingAdvance.Id, 0);
		if (_foundAdvanceId is not 0) await AdvanceData.ClearAdvance(_foundAdvanceId, _transaction.Id);
	}

	private async Task UpdateTransaction()
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

		await TransactionData.UpdateTransaction(new TransactionModel
		{
			Id = _transaction.Id,
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
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			UserId = _transaction.UserId
		});
	}

	#endregion

	#region Printing
	private void PrintTransactionThermal()
	{
		printDocumentCustomer.Print();
		printDocumentMerchant.Print();
	}

	private void printDocumentCustomer_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Customer", _transaction.Id, _foundAdvanceId);

	private void printDocumentMerchant_PrintPage(object sender, PrintPageEventArgs e) => PrintReceipt.DrawGraphics(e, "Merchant", _transaction.Id, _foundAdvanceId);
	#endregion
}
