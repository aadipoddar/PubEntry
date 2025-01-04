namespace PubEntry.Forms.Admin;

public partial class PersonForm : Form
{
	public PersonForm() => InitializeComponent();

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

		return true;
	}
	#endregion

	private async void numberTextBox_TextChanged(object sender, EventArgs e)
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			loyaltyCheckBox.Checked = foundPerson.Loyalty;
		}

		else
		{
			nameTextBox.Text = string.Empty;
			loyaltyCheckBox.Checked = false;
		}
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateFields())
		{
			MessageBox.Show("Please all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		if (await PersonData.LoadPersonByNumber(numberTextBox.Text) != null)
		{
			PersonModel personModel = new()
			{
				Id = 0,
				Name = nameTextBox.Text,
				Number = numberTextBox.Text,
				Loyalty = loyaltyCheckBox.Checked
			};

			await PersonData.PersonUpdate(personModel);

			numberTextBox.Text = string.Empty;
		}

		else MessageBox.Show("Person was not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
}
