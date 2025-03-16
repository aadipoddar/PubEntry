using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Transaction.Advance;

/// <summary>
/// Interaction logic for Advance.xaml
/// </summary>
public partial class AdvanceWindow : Window
{
	private readonly Dashboard _dashboard;
	private readonly int _userId, _locationId;
	private int _foundAdvanceId;
	private readonly List<AdvancePaymentModel> _advancePaymentModels = [];

	public AdvanceWindow(Dashboard dashboard, int locationId, int userId)
	{
		InitializeComponent();
		_dashboard = dashboard;
		_locationId = locationId;
		_userId = userId;
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		_dashboard.Show();
		_dashboard.passwordBox.Focus();
		Close();
	}

	#region LoadData

	private async void Window_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		paymentComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<PaymentModeModel>(TableNames.PaymentMode);
		paymentComboBox.DisplayMemberPath = nameof(PaymentModeModel.Name);
		paymentComboBox.SelectedValuePath = nameof(PaymentModeModel.Id);
		paymentComboBox.SelectedIndex = 0;

		amountDataGrid.ItemsSource = _advancePaymentModels;

		numberTextBox.Focus();

		bookingDatePicker.SelectedDate = DateTime.Now;
		bookingDatePicker.DisplayDateStart = DateTime.Now;
	}

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

	private async void bookingDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e) => await LoadPersonAdvance();

	private async Task LoadPersonAdvance()
	{
		PersonModel foundPerson;
		if (numberComboBox.Visibility == Visibility.Visible && !_isUpdating)
			foundPerson = await PersonData.LoadPersonByNumber(numberComboBox.SelectedValue.ToString());
		else foundPerson = await PersonData.LoadPersonByNumber(numberTextBox.Text);

		if (foundPerson is not null)
		{
			var foundAdvance = await AdvanceData.LoadAdvanceByDateLocationPerson(
				_locationId,
				foundPerson.Id,
				bookingDatePicker.SelectedDate);

			if (foundAdvance is not null)
			{
				_foundAdvanceId = foundAdvance.Id;
				remarksTextBox.Text = foundAdvance.ApprovedBy;
				bookingTextBox.Text = foundAdvance.Booking.ToString();
				_advancePaymentModels.Clear();

				var advanceDetails = await AdvanceData.LoadAdvanceDetailByAdvanceId(foundAdvance.Id);
				foreach (var item in advanceDetails)
					_advancePaymentModels.Add(new AdvancePaymentModel
					{
						PaymentModeId = item.PaymentModeId,
						PaymentModeName = (paymentComboBox.ItemsSource as List<PaymentModeModel>).FirstOrDefault(x => x?.Id == item.PaymentModeId).Name,
						Amount = item.Amount
					});

				CalculateTotalAndRefresh();
				return;
			}
		}

		_foundAdvanceId = 0;
		remarksTextBox.Clear();
		bookingTextBox.Text = "0";
		_advancePaymentModels.Clear();
		CalculateTotalAndRefresh();
	}

	#endregion

	#region DataGrid

	private void addButton_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(amountTextBox.Text)) return;
		var selectedPaymentMode = paymentComboBox.SelectedItem as PaymentModeModel;

		if (_advancePaymentModels.Any(x => x.PaymentModeId == selectedPaymentMode.Id))
			_advancePaymentModels.FirstOrDefault(x => x.PaymentModeId == selectedPaymentMode.Id).Amount += int.Parse(amountTextBox.Text);

		else _advancePaymentModels.Add(new AdvancePaymentModel
		{
			PaymentModeId = selectedPaymentMode.Id,
			PaymentModeName = selectedPaymentMode.Name,
			Amount = int.Parse(amountTextBox.Text)
		});

		CalculateTotalAndRefresh();
	}

	private void CalculateTotalAndRefresh()
	{
		amountDataGrid.Items.Refresh();
		amountTextBox.Text = "0";
		totalTextBox.Text = _advancePaymentModels.Sum(x => x.Amount).ToString();
	}

	private void amountDataGrid_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (amountDataGrid.SelectedItem is AdvancePaymentModel selectedPayment)
		{
			paymentComboBox.SelectedItem = paymentComboBox.Items.Cast<PaymentModeModel>()
				.FirstOrDefault(x => x?.Id == selectedPayment.PaymentModeId);
			_advancePaymentModels.Remove(selectedPayment);
			CalculateTotalAndRefresh();
			amountTextBox.Text = selectedPayment.Amount.ToString();
		}
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
		if (string.IsNullOrEmpty(numberTextBox.Text) && numberComboBox.Visibility == Visibility.Hidden) return false;
		if (string.IsNullOrEmpty(nameTextBox.Text)) return false;
		if (string.IsNullOrEmpty(bookingTextBox.Text)) bookingTextBox.Text = "0";

		return true;
	}

	#endregion

	#region Saving

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		if (_foundAdvanceId != 0)
		{
			await UpdateAdvance();
			await DeleteAndInsertAdvanceDetail();
		}

		else await InsertAdvanceDetail(await InsertAdvance());

		await ClearForm();
	}

	private async Task<int> InsertAdvance()
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

		return await AdvanceData.InsertAdvance(new AdvanceModel
		{
			Id = 0,
			LocationId = _locationId,
			PersonId = personModel.Id,
			DateTime = DateTime.Now,
			AdvanceDate = bookingDatePicker.SelectedDate.Value,
			Booking = int.Parse(bookingTextBox.Text),
			ApprovedBy = remarksTextBox.Text,
			UserId = _userId,
			TransactionId = 0
		});
	}

	private async Task InsertAdvanceDetail(int advanceId)
	{
		foreach (var item in _advancePaymentModels)
			await AdvanceData.InsertAdvanceDetail(new AdvanceDetailModel
			{
				Id = 0,
				AdvanceId = advanceId,
				PaymentModeId = item.PaymentModeId,
				Amount = item.Amount
			});
	}

	private async Task UpdateAdvance() =>
		await AdvanceData.UpdateAdvance(new AdvanceModel
		{
			Id = _foundAdvanceId,
			LocationId = _locationId,
			PersonId = (await PersonData.LoadPersonByNumber(numberTextBox.Text)).Id,
			DateTime = DateTime.Now,
			AdvanceDate = bookingDatePicker.SelectedDate.Value,
			Booking = int.Parse(bookingTextBox.Text),
			ApprovedBy = remarksTextBox.Text,
			UserId = _userId,
			TransactionId = 0
		});

	private async Task DeleteAndInsertAdvanceDetail()
	{
		await AdvanceData.DeleteAdvanceDetails(_foundAdvanceId);
		await InsertAdvanceDetail(_foundAdvanceId);
	}

	private async Task ClearForm()
	{
		numberTextBox.Visibility = Visibility.Visible;
		numberComboBox.Visibility = Visibility.Hidden;
		_isUpdating = true;
		nameTextBox.Clear();
		numberTextBox.Focus();
		numberTextBox.Clear();
		_isUpdating = false;

		await LoadPersonAdvance();
	}

	#endregion
}
