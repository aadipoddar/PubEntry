namespace PubEntry;

public partial class AdvanceEntryForm : Form
{
	public AdvanceEntryForm() => InitializeComponent();

	private void AdvanceEntryForm_Load(object sender, EventArgs e) => LoadData();

	public async void LoadData()
	{
		locationComboBox.DataSource = await LocationData.LoadActiveLocations();
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		advanceDateTimePicker.MinDate = DateTime.Now;

		paymentModeComboBox.DataSource = await PaymentModeData.LoadActivePaymentModes();
		paymentModeComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentModeComboBox.ValueMember = nameof(PaymentModeModel.Id);
		paymentModeComboBox.SelectedIndex = 0;

		amountDataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
	}

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

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private bool ValidateFields()
	{
		if (string.IsNullOrWhiteSpace(numberTextBox.Text)) return false;
		if (string.IsNullOrWhiteSpace(nameTextBox.Text)) return false;
		if (string.IsNullOrWhiteSpace(bookingAmountTextBox.Text)) return false;
		if (amountDataGridView.Rows.Count == 0) return false;

		int totalAmount = amountDataGridView.Rows.Cast<DataGridViewRow>().Sum(row => int.Parse(row.Cells["Amount"].Value.ToString()));
		if (totalAmount > int.Parse(bookingAmountTextBox.Text)) return false;

		return true;
	}

	private void ClearForm()
	{
		numberTextBox.Text = string.Empty;
		nameTextBox.Text = string.Empty;
		loyaltyCheckBox.Checked = false;
		bookingAmountTextBox.Text = "0";
		locationComboBox.SelectedIndex = 0;
		advanceDateTimePicker.MinDate = DateTime.Now;
		paymentModeComboBox.SelectedIndex = 0;
		amountDataGridView.Rows.Clear();
		numberTextBox.Focus();
	}

	private async void saveButton_Click(object sender, EventArgs e)
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

		int advanceId = await TransactionData.AdvanceInsert(new AdvanceModel
		{
			Id = 0,
			TransactionId = 0,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = personModel.Id,
			DateTime = DateTime.Now,
			AdvanceDate = advanceDateTimePicker.Value,
			BookingAmount = int.Parse(bookingAmountTextBox.Text),
			Cleared = false
		});

		foreach (DataGridViewRow row in amountDataGridView.Rows)
		{
			await PaymentData.PaymentInsert(new PaymentModel
			{
				Id = 0,
				TransactionId = 0,
				AdvanceId = advanceId,
				PaymentModeId = int.Parse(row.Cells[0].Value.ToString()),
				Amount = int.Parse(row.Cells[2].Value.ToString())
			});
		}

		ClearForm();
	}

	private void addButton_Click(object sender, EventArgs e)
	{
		amountDataGridView.Rows.Add
			((paymentModeComboBox.SelectedItem as PaymentModeModel).Id,
			(paymentModeComboBox.SelectedItem as PaymentModeModel).Name,
			amountTextBox.Text);

		amountTextBox.Text = "0";
	}
}
