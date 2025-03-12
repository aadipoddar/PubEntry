using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using PubEntryWPF.Transaction.Printing;

namespace PubEntryWPF.Transaction;

/// <summary>
/// Interaction logic for UpdateTransaction.xaml
/// </summary>
public partial class UpdateTransactionPage : Page
{
	private readonly TransactionModel _transaction;
	private readonly Frame _parentFrame;
	private int _foundAdvanceId;

	public UpdateTransactionPage(TransactionModel transaction, Frame parentFrame)
	{
		InitializeComponent();
		_transaction = transaction;
		_parentFrame = parentFrame;
	}

	#region LoadData

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);

		reservationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<ReservationTypeModel>(TableNames.ReservationType);
		reservationComboBox.DisplayMemberPath = nameof(ReservationTypeModel.Name);
		reservationComboBox.SelectedValuePath = nameof(ReservationTypeModel.Id);

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
				dateTimeLabel.Content = _transaction.DateTime.ToString();
				reservationComboBox.SelectedValue = _transaction.ReservationTypeId;
				remarksTextBox.Text = _transaction.ApprovedBy.ToString();
				locationComboBox.SelectedValue = _transaction.LocationId;
			}
			else MessageBox.Show("Something is Seriously Wrong. Contact Developer", "Cannot Load Data");
		}
		else MessageBox.Show("Something is Seriously Wrong. Contact Developer", "Cannot Load Data");
	}

	private async void locationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadPersonAdvance();

	private bool _isUpdating = false;

	private async void numberTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (_isUpdating) return;
		_isUpdating = true;

		var previousName = nameTextBox.Text;

		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is not null)
		{
			nameTextBox.Text = foundPerson.Name;
			loyaltyCheckBox.IsChecked = foundPerson.Loyalty;
		}
		else
		{
			nameTextBox.Text = previousName;
			loyaltyCheckBox.IsChecked = false;
		}

		await LoadPersonAdvance();
		_isUpdating = false;
	}

	private async void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (_isUpdating) return;
		_isUpdating = true;

		var previousNumber = numberTextBox.Text;

		var foundPersons = await PersonData.LoadPersonByName(nameTextBox.Text);

		if (foundPersons.Count > 0)
		{
			if (foundPersons.Count > 1)
			{
				numberTextBox.Visibility = Visibility.Hidden;
				numberComboBox.Visibility = Visibility.Visible;
				numberComboBox.ItemsSource = foundPersons;
				numberComboBox.DisplayMemberPath = nameof(PersonModel.Number);
				numberComboBox.SelectedValuePath = nameof(PersonModel.Number);
				numberComboBox.SelectedIndex = 0;
			}
			else
			{
				numberTextBox.Visibility = Visibility.Visible;
				numberComboBox.Visibility = Visibility.Hidden;
				numberTextBox.Text = foundPersons[0].Number;
				loyaltyCheckBox.IsChecked = foundPersons[0].Loyalty;
			}
		}
		else
		{
			numberTextBox.Visibility = Visibility.Visible;
			numberComboBox.Visibility = Visibility.Hidden;
			numberTextBox.Text = previousNumber;
			loyaltyCheckBox.IsChecked = false;
		}

		await LoadPersonAdvance();
		_isUpdating = false;
	}

	private async void numberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (_isUpdating) return;
		_isUpdating = true;

		if (numberComboBox.Visibility == Visibility.Visible && numberComboBox.SelectedValue != null)
		{
			string selectedNumber = numberComboBox.SelectedValue.ToString();
			var foundPerson = await PersonData.LoadPersonByNumber(selectedNumber);
			if (foundPerson is not null)
				loyaltyCheckBox.IsChecked = foundPerson.Loyalty;

			await LoadPersonAdvance();
		}

		_isUpdating = false;
	}

	private async Task LoadPersonAdvance()
	{
		PersonModel foundPerson;
		if (numberComboBox.Visibility == Visibility.Visible && !_isUpdating)
			foundPerson = await PersonData.LoadPersonByNumber(numberComboBox.SelectedValue.ToString());
		else foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is not null)
		{
			AdvanceModel foundAdvance;

			if (foundPerson.Id == _transaction.PersonId && (locationComboBox.SelectedItem as LocationModel).Id == _transaction.LocationId)
			{
				foundAdvance = await AdvanceData.LoadAdvanceByTransactionId(_transaction.Id);
				if (foundAdvance is not null)
				{
					_foundAdvanceId = foundAdvance.Id;
					advancePanel.Visibility = Visibility.Visible;
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
					advancePanel.Visibility = Visibility.Visible;
					remarksTextBox.Text = foundAdvance.ApprovedBy;
					bookingTextBox.Text = foundAdvance.Booking.ToString();
					var advanceDetail = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
					advanceTextBox.Text = advanceDetail.Sum(x => x.Amount).ToString();
					return;
				}
			}
		}

		_foundAdvanceId = 0;
		advancePanel.Visibility = Visibility.Hidden;
		remarksTextBox.Text = _transaction.ApprovedBy;
	}

	#endregion

	#region Validation

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private bool ValidateFields()
	{
		if (string.IsNullOrEmpty(numberTextBox.Text) && numberComboBox.Visibility == Visibility.Hidden) return false;
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

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateFields())
		{
			MessageBox.Show("Please all Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		await UpdateTransaction();
		await UpdateAdvance();
		await PrintThermal();

		_parentFrame.Content = new SlipIdPage(_parentFrame);
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
			Number = numberComboBox.Visibility == Visibility.Visible ? numberComboBox.SelectedValue.ToString() : numberTextBox.Text,
			Loyalty = (bool)loyaltyCheckBox.IsChecked
		};

		PersonModel foundPerson;
		if (numberComboBox.Visibility == Visibility.Visible && !_isUpdating)
			foundPerson = await PersonData.LoadPersonByNumber(numberComboBox.SelectedValue.ToString());
		else foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is null) personModel.Id = await PersonData.InsertPerson(personModel);
		else personModel.Id = await PersonData.UpdatePerson(personModel);

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
			ReservationTypeId = (int)reservationComboBox.SelectedValue,
			DateTime = DateTime.Now,
			ApprovedBy = remarksTextBox.Text,
			LocationId = (int)locationComboBox.SelectedValue,
			UserId = _transaction.UserId
		});
	}

	private async Task PrintThermal()
	{
		var receiptModel = await CommonData.LoadTableDataById<TransactionPrintModel>(ViewNames.Transactions, _transaction.Id);

		int advance = 0;
		if (_foundAdvanceId is not 0) advance = (await AdvanceData.LoadAdvanceDetailByAdvanceId(_foundAdvanceId)).Sum(x => x.Amount);

		PrintDialog printDialog = new();

		IDocumentPaginatorSource idpSource = ThermalReceipt.Print(receiptModel, "Customer", advance);
		printDialog.PrintDocument(idpSource.DocumentPaginator, "Thermal Receipt Customer");

		idpSource = ThermalReceipt.Print(receiptModel, "Merchant", advance);
		printDialog.PrintDocument(idpSource.DocumentPaginator, "Thermal Receipt Merchant");
	}

	#endregion
}
