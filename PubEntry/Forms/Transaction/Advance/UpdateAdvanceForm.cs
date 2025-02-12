using System.Reflection;

namespace PubEntry.Forms.Transaction.Advance;

public partial class UpdateAdvanceForm : Form
{
	private readonly AdvanceModel _updateAdvanceModel;
	private int _foundAdvanceId;

	public UpdateAdvanceForm(AdvanceModel updateAdvanceModel)
	{
		_updateAdvanceModel = updateAdvanceModel;
		InitializeComponent();
	}

	#region LoadData

	private async void UpdateAdvance_Load(object sender, EventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		locationComboBox.SelectedValue = _updateAdvanceModel.LocationId;
		advanceDateTimePicker.Value = _updateAdvanceModel.AdvanceDate;
		approvedByTextBox.Text = _updateAdvanceModel.ApprovedBy;
		numberTextBox.Text = (await CommonData.LoadTableDataById<PersonModel>(Table.Person, _updateAdvanceModel.PersonId)).Number;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
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

	private async void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) =>
		await LoadPersonAdvance();

	private async void advanceDateTimePicker_ValueChanged(object sender, EventArgs e) => await LoadPersonAdvance();

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
				_foundAdvanceId = foundAdvance.Id;
				approvedByTextBox.Text = foundAdvance.ApprovedBy;

				if (_foundAdvanceId == _updateAdvanceModel.Id) return;

				MessageBox.Show("Advance Present for this Person and Date, Please Check Again");
				Close();
			}
		}

		_foundAdvanceId = 0;
		approvedByTextBox.Text = string.Empty;
	}

	#endregion

	#region Validation

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;

		return true;
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

		await InsertPerson();
		await UpdateAdvance();

		Close();
	}

	private async Task InsertPerson()
	{
		if (await PersonData.LoadPersonByNumber(numberTextBox.Text) is null)
			await PersonData.InsertPerson(new PersonModel
			{
				Id = 0,
				Number = numberTextBox.Text,
				Name = nameTextBox.Text,
				Loyalty = loyaltyCheckBox.Checked
			});
	}

	private async Task UpdateAdvance() =>
		await AdvanceData.UpdateAdvance(new AdvanceModel
		{
			Id = _updateAdvanceModel.Id,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = (await PersonData.LoadPersonByNumber(numberTextBox.Text)).Id,
			DateTime = _updateAdvanceModel.DateTime,
			AdvanceDate = advanceDateTimePicker.Value.Date,
			Booking = _updateAdvanceModel.Booking,
			ApprovedBy = approvedByTextBox.Text,
			TransactionId = _updateAdvanceModel.TransactionId
		});

	#endregion
}