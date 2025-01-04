namespace PubEntry.Forms.Transaction.Advance;

public partial class AdvanceForm : Form
{
	private int foundAdvanceId = 0;

	public AdvanceForm() => InitializeComponent();

	#region LoadData
	private void AdvanceForm_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>("LocationTable", true);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		paymentComboBox.DataSource = await CommonData.LoadTableDataByStatus<PaymentModeModel>("PaymentModeTable", true);
		paymentComboBox.DisplayMember = nameof(PaymentModeModel.Name);
		paymentComboBox.ValueMember = nameof(PaymentModeModel.Id);

		advanceDateTimePicker.MinDate = DateTime.Now;
	}

	private async void numberTextBox_TextChanged(object sender, EventArgs e)
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

	private async void advanceDateTimePicker_ValueChanged(object sender, EventArgs e) => await LoadPersonAdvance();

	private async void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) => await LoadPersonAdvance();

	private async Task LoadPersonAdvance()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson is not null)
		{
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(
				(locationComboBox.SelectedItem as LocationModel).Id,
				foundPerson.Id,
				advanceDateTimePicker.Value.Date);

			if (foundAdvance is not null)
			{
				foundAdvanceId = foundAdvance.Id;
				approvedByTextBox.Text = foundAdvance.ApprovedBy;
				bookingTextBox.Text = foundAdvance.Booking.ToString();
				amountDataGridView.Rows.Clear();

				var advanceDetails = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
				foreach (var item in advanceDetails)
					amountDataGridView.Rows.Add(item.PaymentMode,
						(paymentComboBox.DataSource as List<PaymentModeModel>).FirstOrDefault(x => x.Id == item.PaymentMode).Name,
						item.Amount);

				CalculateTotalAmount();
				return;
			}
		}
		ClearAdvances();
	}

	private void ClearAdvances()
	{
		foundAdvanceId = 0;
		approvedByTextBox.Text = string.Empty;
		bookingTextBox.Text = "0";
		amountDataGridView.Rows.Clear();
		CalculateTotalAmount();
	}
	#endregion

	#region Validation
	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private void textBox_Click(object sender, EventArgs e) => (sender as TextBox).SelectAll();

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;
		if (string.IsNullOrEmpty(bookingTextBox.Text) || int.Parse(bookingTextBox.Text) == 0) bookingTextBox.Text = totalTextBox.Text;
		if (amountDataGridView.Rows.Count == 0) return false;

		return true;
	}

	private void addButton_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(amountTextBox.Text)) return;

		var selectedPaymentMode = paymentComboBox.SelectedItem as PaymentModeModel;
		var existingRow = amountDataGridView.Rows
			.Cast<DataGridViewRow>()
			.FirstOrDefault(row => (int)row.Cells[0].Value == selectedPaymentMode.Id);

		if (existingRow != null) existingRow.Cells[2].Value = (int)existingRow.Cells[2].Value + int.Parse(amountTextBox.Text);
		else amountDataGridView.Rows.Add(selectedPaymentMode.Id, selectedPaymentMode.Name, int.Parse(amountTextBox.Text));

		amountTextBox.Text = "0";
		CalculateTotalAmount();
	}

	private void amountDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1) return;
		amountTextBox.Text = amountDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
		paymentComboBox.SelectedItem = (paymentComboBox.DataSource as List<PaymentModeModel>).FirstOrDefault(x => x.Id == (int)amountDataGridView.Rows[e.RowIndex].Cells[0].Value);
		amountDataGridView.Rows.RemoveAt(e.RowIndex);
		CalculateTotalAmount();
	}

	private void CalculateTotalAmount()
	{
		int total = 0;
		foreach (DataGridViewRow row in amountDataGridView.Rows)
			total += (int)row.Cells[2].Value;
		totalTextBox.Text = total.ToString();
	}
	#endregion

	#region Saving
	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		if (foundAdvanceId != 0)
		{
			await UpdateAdvanceTable();
			await DeleteAndInsertAdvanceDetail();
		}

		else await InsertIntoAdvanceDetailTable(await InsertIntoAdvanceTable());

		ClearForm();
	}

	private async Task<int> InsertIntoAdvanceTable()
	{
		PersonModel personModel = new()
		{
			Id = 0,
			Name = nameTextBox.Text,
			Number = numberTextBox.Text,
			Loyalty = loyaltyCheckBox.Checked
		};

		if (nameTextBox.ReadOnly == false) personModel.Id = await PersonData.PersonInsert(personModel);
		personModel.Id = await PersonData.PersonUpdate(personModel);

		return await AdvanceData.AdvanceInsert(new AdvanceModel
		{
			Id = 0,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = personModel.Id,
			DateTime = DateTime.Now,
			AdvanceDate = advanceDateTimePicker.Value.Date,
			Booking = int.Parse(bookingTextBox.Text),
			ApprovedBy = approvedByTextBox.Text,
			TransactionId = 0
		});
	}

	private async Task UpdateAdvanceTable() =>
		await AdvanceData.AdvanceUpdate(new AdvanceModel
		{
			Id = foundAdvanceId,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = (await PersonData.LoadPersonByNumber(numberTextBox.Text)).Id,
			DateTime = DateTime.Now,
			AdvanceDate = advanceDateTimePicker.Value.Date,
			Booking = int.Parse(bookingTextBox.Text),
			ApprovedBy = approvedByTextBox.Text,
			TransactionId = 0
		});

	private async Task InsertIntoAdvanceDetailTable(int advanceId)
	{
		foreach (DataGridViewRow row in amountDataGridView.Rows)
			await AdvanceData.AdvanceDetailInsert(new AdvanceDetailModel
			{
				Id = 0,
				AdvanceId = advanceId,
				PaymentMode = (int)row.Cells[0].Value,
				Amount = (int)row.Cells[2].Value
			});
	}

	private async Task DeleteAndInsertAdvanceDetail()
	{
		await AdvanceData.AdvanceDetailDeleteByAdvanceId(foundAdvanceId);
		await InsertIntoAdvanceDetailTable(foundAdvanceId);
	}

	private void ClearForm()
	{
		numberTextBox.Text = string.Empty;
		bookingTextBox.Text = string.Empty;
		approvedByTextBox.Text = string.Empty;

		amountDataGridView.Rows.Clear();

		CalculateTotalAmount();
		numberTextBox.Focus();
	}
	#endregion
}
