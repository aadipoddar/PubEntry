using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry.Admin;

public partial class PersonForm : Form
{
	public PersonForm()
	{
		InitializeComponent();
	}

	private void ClearForm()
	{
		numberTextBox.Text = string.Empty;
		nameTextBox.Text = string.Empty;
		loyaltyCheckBox.Checked = false;
		numberTextBox.Focus();
	}


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
		var foundPerson = await PersonData.GetPersonByNumber(numberTextBox.Text);
		if (foundPerson != null)
		{
			nameTextBox.Text = foundPerson.Name;
			if (foundPerson.Loyalty == 1) loyaltyCheckBox.Checked = true;
			else loyaltyCheckBox.Checked = false;
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
			MessageBox.Show("Enter all Fields");
			return;
		}

		var foundPerson = await PersonData.GetPersonByNumber(numberTextBox.Text);
		if (foundPerson != null)
		{
			PersonModel personModel = new();
			personModel.Id = 0;
			personModel.Name = nameTextBox.Text;
			personModel.Number = numberTextBox.Text;
			personModel.Loyalty = loyaltyCheckBox.Checked ? 1 : 0;

			personModel.Id = await PersonData.UpdatePerson(personModel);

			ClearForm();
		}

		else MessageBox.Show("Person not found");
	}
}
