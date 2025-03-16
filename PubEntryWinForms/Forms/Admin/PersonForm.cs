using System.Reflection;

namespace PubEntry.Forms.Admin;

public partial class PersonForm : Form
{
	public PersonForm() => InitializeComponent();

	private void PersonForm_Load(object sender, EventArgs e) =>
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

	#region Validation
	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private bool ValidateFields() =>
		!string.IsNullOrEmpty(numberTextBox.Text) && !string.IsNullOrEmpty(nameTextBox.Text);
	#endregion

	private async void numberTextBox_TextChanged(object sender, EventArgs e)
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
		if (foundPerson is not null)
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

			await PersonData.UpdatePerson(personModel);

			numberTextBox.Text = string.Empty;
		}

		else MessageBox.Show("Person was not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
}
