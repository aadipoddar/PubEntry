using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntry.Transaction.Advance;

/// <summary>
/// Interaction logic for UpdateAdvance.xaml
/// </summary>
public partial class UpdateAdvancePage : Page
{
	private readonly AdvanceModel _advance;
	private readonly Frame _parentFrame;
	private int _foundAdvanceId;

	public UpdateAdvancePage(AdvanceModel advance, Frame parentFrame)
	{
		InitializeComponent();
		_advance = advance;
		_parentFrame = parentFrame;
	}

	#region LoadData

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);

		locationComboBox.SelectedValue = _advance.LocationId;
		bookingDatePicker.SelectedDate = _advance.AdvanceDate;
		remarksTextBox.Text = _advance.ApprovedBy;
		numberTextBox.Text = (await CommonData.LoadTableDataById<PersonModel>(TableNames.Person, _advance.PersonId)).Number;
	}

	private async void numberTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is not null)
		{
			nameTextBox.Text = foundPerson.Name;
			nameTextBox.IsReadOnly = true;
			loyaltyCheckBox.IsChecked = foundPerson.Loyalty;
		}
		else
		{
			nameTextBox.Clear();
			nameTextBox.IsReadOnly = false;
			loyaltyCheckBox.IsChecked = false;
		}

		await LoadPersonAdvance();
	}

	private async void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadPersonAdvance();

	private async void bookingDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => await LoadPersonAdvance();

	private async Task LoadPersonAdvance()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is not null)
		{
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(
				(int)locationComboBox.SelectedValue,
				foundPerson.Id,
				bookingDatePicker.SelectedDate);

			if (foundAdvance is not null)
			{
				_foundAdvanceId = foundAdvance.Id;
				remarksTextBox.Text = foundAdvance.ApprovedBy;

				if (_foundAdvanceId == _advance.Id) return;

				MessageBox.Show("Advance Present for this Person and Date, Please Check Again");
				await LoadData();
			}
		}

		_foundAdvanceId = 0;
		remarksTextBox.Text = _advance.ApprovedBy;
	}

	#endregion

	#region Validation

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;

		return true;
	}

	#endregion

	#region Saving 

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		await InsertPerson();
		await AdvanceUpdate();

		_parentFrame.Content = new AdvanceIdPage(_parentFrame);
	}

	private async Task InsertPerson()
	{
		PersonModel personModel = new()
		{
			Id = 0,
			Name = nameTextBox.Text,
			Number = numberTextBox.Text,
			Loyalty = (bool)loyaltyCheckBox.IsChecked
		};

		if (nameTextBox.IsReadOnly == false) personModel.Id = await PersonData.InsertPerson(personModel);
		else personModel.Id = (await PersonData.LoadPersonByNumber(numberTextBox.Text)).Id;
		await PersonData.UpdatePerson(personModel);
	}

	private async Task AdvanceUpdate() =>
		await AdvanceData.UpdateAdvance(new AdvanceModel
		{
			Id = _advance.Id,
			LocationId = (locationComboBox.SelectedItem as LocationModel).Id,
			PersonId = (await PersonData.LoadPersonByNumber(numberTextBox.Text)).Id,
			DateTime = _advance.DateTime,
			AdvanceDate = bookingDatePicker.SelectedDate.Value,
			Booking = _advance.Booking,
			ApprovedBy = remarksTextBox.Text,
			TransactionId = _advance.TransactionId
		});

	#endregion
}
