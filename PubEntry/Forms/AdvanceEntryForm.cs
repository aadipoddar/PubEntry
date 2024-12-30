namespace PubEntry;

public partial class AdvanceEntryForm : Form
{
	public AdvanceEntryForm() => InitializeComponent();

	private void AdvanceEntryForm_Load(object sender, EventArgs e) => LoadComboBox();

	public async Task LoadComboBox()
	{
		locationComboBox.DataSource = await LocationData.LoadActiveLocations();
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		advanceDateTimePicker.MinDate = DateTime.Now;
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
		if (string.IsNullOrWhiteSpace(amountTextBox.Text)) amountTextBox.Text = "0";

		return true;
	}

	private void ClearForm()
	{
		numberTextBox.Text = string.Empty;
		nameTextBox.Text = string.Empty;
		loyaltyCheckBox.Checked = false;
		amountTextBox.Text = "0";
		locationComboBox.SelectedIndex = 0;
		advanceDateTimePicker.MinDate = DateTime.Now;
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

		await TransactionData.AdvanceInsert(new AdvanceModel
		{
			Id = 0,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = personModel.Id,
			Amount = int.Parse(amountTextBox.Text),
			AdvanceDate = advanceDateTimePicker.Value,
			Cleared = false
		});

		ClearForm();
	}
}
