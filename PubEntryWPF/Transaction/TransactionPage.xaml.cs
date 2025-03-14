using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

using PubEntryWPF.Transaction.Printing;

namespace PubEntryWPF.Transaction;

/// <summary>
/// Interaction logic for Transaction.xaml
/// </summary>
public partial class TransactionPage : Window
{
	#region Timers

	private readonly DispatcherTimer _inactivityTimer = new() { Interval = TimeSpan.FromMinutes((int)Application.Current.Resources[SettingsKeys.InactivityTime]) };
	private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };

	private void InitializeTimers()
	{
		_timer.Tick += (sender, e) => dateTimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss tt");
		_timer.Start();

		_inactivityTimer.Tick += (sender, e) => Close();
		_inactivityTimer.Start();
	}

	private void ResetInactivityTimer(object sender, EventArgs e)
	{
		_inactivityTimer.Stop();
		_inactivityTimer.Start();
	}

	private void SubscribeToTextChangedEvents()
	{
		numberTextBox.TextChanged += ResetInactivityTimer;
		nameTextBox.TextChanged += ResetInactivityTimer;
		loyaltyCheckBox.Checked += ResetInactivityTimer;
		loyaltyCheckBox.Unchecked += ResetInactivityTimer;
		maleTextBox.TextChanged += ResetInactivityTimer;
		femaleTextBox.TextChanged += ResetInactivityTimer;
		cashTextBox.TextChanged += ResetInactivityTimer;
		cardTextBox.TextChanged += ResetInactivityTimer;
		upiTextBox.TextChanged += ResetInactivityTimer;
		amexTextBox.TextChanged += ResetInactivityTimer;
		remarksTextBox.TextChanged += ResetInactivityTimer;
		reservationComboBox.SelectionChanged += ResetInactivityTimer;
		saveButton.Click += ResetInactivityTimer;
	}

	#endregion

	#region Initial Loading

	private readonly Dashboard _dashboard;
	private readonly int _userId, _locationId;
	private int _transactionId, _foundAdvanceId;

	public TransactionPage(Dashboard dashboard, int locationId, int userId)
	{
		InitializeComponent();
		_dashboard = dashboard;
		_userId = userId;
		_locationId = locationId;
	}

	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		await LoadData();
		InitializeTimers();
		SubscribeToTextChangedEvents();
	}

	private async Task LoadData()
	{
		reservationComboBox.ItemsSource = await CommonData.LoadTableData<ReservationTypeModel>(TableNames.ReservationType);
		reservationComboBox.DisplayMemberPath = nameof(ReservationTypeModel.Name);
		reservationComboBox.SelectedValuePath = nameof(ReservationTypeModel.Id);
		reservationComboBox.SelectedIndex = 0;

		numberTextBox.Focus();
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		_dashboard.Show();
		_dashboard.passwordBox.Focus();
		Close();
	}

	#endregion

	#region LoadPersonAndAdvance

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
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(_locationId, foundPerson.Id);
			if (foundAdvance is not null)
			{
				_foundAdvanceId = foundAdvance.Id;
				advancePanel.Visibility = Visibility.Visible;
				remarksTextBox.Text = foundAdvance.ApprovedBy;
				bookingTextBox.Text = foundAdvance.Booking.ToString();
				var advanceDetail = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
				advanceTextBox.Text = advanceDetail.Sum(x => x?.Amount).ToString();

				return;
			}
		}

		_foundAdvanceId = 0;
		advancePanel.Visibility = Visibility.Hidden;
		remarksTextBox.Clear();
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
			MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		await InsertTransaction();
		if (_foundAdvanceId is not 0) await AdvanceData.ClearAdvance(_foundAdvanceId, _transactionId);

		await PrintThermal();
		ClearForm();
	}

	private async Task InsertTransaction()
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
		else
		{
			personModel.Id = foundPerson.Id;
			await PersonData.UpdatePerson(personModel);
		}

		_transactionId = await TransactionData.InsertTransaction(new TransactionModel
		{
			Id = 0,
			PersonId = personModel.Id,
			Male = int.Parse(maleTextBox.Text),
			Female = int.Parse(femaleTextBox.Text),
			Cash = int.Parse(cashTextBox.Text),
			Card = int.Parse(cardTextBox.Text),
			UPI = int.Parse(upiTextBox.Text),
			Amex = int.Parse(amexTextBox.Text),
			ReservationTypeId = int.Parse(reservationComboBox.SelectedValue.ToString()),
			DateTime = DateTime.Now,
			ApprovedBy = remarksTextBox.Text,
			LocationId = _locationId,
			UserId = _userId
		});
	}

	private async Task PrintThermal()
	{
		var receiptModel = await CommonData.LoadTableDataById<TransactionPrintModel>(ViewNames.Transactions, _transactionId);

		int advance = 0;
		if (_foundAdvanceId is not 0) advance = (await AdvanceData.LoadAdvanceDetailByAdvanceId(_foundAdvanceId)).Sum(x => x.Amount);

		PrintDialog printDialog = new();

		IDocumentPaginatorSource idpSource = ThermalReceipt.Print(receiptModel, "Customer", advance);
		printDialog.PrintDocument(idpSource.DocumentPaginator, "Thermal Receipt Customer");

		idpSource = ThermalReceipt.Print(receiptModel, "Merchant", advance);
		printDialog.PrintDocument(idpSource.DocumentPaginator, "Thermal Receipt Merchant");
	}

	private void ClearForm()
	{
		numberTextBox.Visibility = Visibility.Visible;
		numberComboBox.Visibility = Visibility.Hidden;
		_isUpdating = true;
		nameTextBox.Text = string.Empty;
		numberTextBox.Text = string.Empty;
		numberTextBox.Focus();
		loyaltyCheckBox.IsChecked = false;

		remarksTextBox.Clear();
		reservationComboBox.SelectedIndex = 0;

		maleTextBox.Text = "0";
		femaleTextBox.Text = "0";

		cashTextBox.Text = "0";
		cardTextBox.Text = "0";
		upiTextBox.Text = "0";
		amexTextBox.Text = "0";

		_foundAdvanceId = 0;
		advancePanel.Visibility = Visibility.Hidden;
		_transactionId = 0;
		_isUpdating = false;
	}

	#endregion
}
