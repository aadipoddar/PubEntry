using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;

namespace PubEntryWPF.Transaction;

/// <summary>
/// Interaction logic for Transaction.xaml
/// </summary>
public partial class Transaction : Window
{
	#region Timers

	private readonly DispatcherTimer _inactivityTimer = new() { Interval = TimeSpan.FromMinutes(int.Parse(Task.Run(async () => await SettingsData.LoadSettingsByKey(SettingsKeys.InactivityTime)).Result)) };
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

	public Transaction(Dashboard dashboard, int locationId, int userId)
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
		reservationComboBox.ItemsSource = await CommonData.LoadTableData<ReservationTypeModel>(Table.ReservationType);
		reservationComboBox.DisplayMemberPath = nameof(ReservationTypeModel.Name);
		reservationComboBox.SelectedValuePath = nameof(ReservationTypeModel.Id);
		reservationComboBox.SelectedIndex = 0;

		numberTextBox.Focus();

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		_dashboard.Show();
		_dashboard.passwordBox.Focus();
		Close();
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
		if (string.IsNullOrEmpty(numberTextBox.Text)) return false;
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

	#region LoadAdvance

	private async void numberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
			nameTextBox.Text = string.Empty;
			nameTextBox.IsReadOnly = false;
			loyaltyCheckBox.IsChecked = false;
		}

		await LoadPersonAdvance();
	}

	private async Task LoadPersonAdvance()
	{
		var foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);
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
		ClearForm();
	}

	private async Task InsertTransaction()
	{
		PersonModel personModel = new()
		{
			Id = 0,
			Name = nameTextBox.Text,
			Number = numberTextBox.Text,
			Loyalty = (bool)loyaltyCheckBox.IsChecked
		};

		if (nameTextBox.IsReadOnly == false) personModel.Id = await PersonData.InsertPerson(personModel);
		personModel.Id = await PersonData.UpdatePerson(personModel);

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

	private void ClearForm()
	{
		numberTextBox.Clear();
		nameTextBox.Clear();
		nameTextBox.IsReadOnly = false;
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
	}

	#endregion
}
