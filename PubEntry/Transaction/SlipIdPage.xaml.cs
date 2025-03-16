using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntry.Transaction;

/// <summary>
/// Interaction logic for SlipId.xaml
/// </summary>
public partial class SlipIdPage : Page
{
	private static int PubOpenTime => (int)Application.Current.Resources[SettingsKeys.PubOpenTime];
	private static int PubCloseTime => (int)Application.Current.Resources[SettingsKeys.PubCloseTime];

	private readonly Frame _parentFrame;

	public SlipIdPage(Frame parentFrame)
	{
		InitializeComponent();
		_parentFrame = parentFrame;
	}

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		if (DateTime.Now.Hour >= PubOpenTime)
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddDays(1);
			fromDatePicker.SelectedDate = DateTime.Now.Date;
		}
		else
		{
			toDatePicker.SelectedDate = DateTime.Now.Date;
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddDays(-1);
		}

		List<int> hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
		List<string> slots = ["AM", "PM"];

		fromTimePicker.ItemsSource = hours;
		toTimePicker.ItemsSource = hours;
		fromSlotPicker.ItemsSource = slots;
		toSlotPicker.ItemsSource = slots;

		if (PubOpenTime >= 12)
		{
			fromSlotPicker.SelectedItem = "PM";
			if (PubOpenTime > 12) fromTimePicker.SelectedItem = PubOpenTime - 12;
			else fromTimePicker.SelectedItem = PubOpenTime;
		}

		else
		{
			fromSlotPicker.SelectedItem = "AM";
			fromTimePicker.SelectedItem = PubOpenTime;
		}

		if (PubCloseTime >= 12)
		{
			toSlotPicker.SelectedItem = "PM";
			if (PubCloseTime > 12) toTimePicker.SelectedItem = PubCloseTime - 12;
			else toTimePicker.SelectedItem = PubCloseTime;
		}
		else
		{
			toSlotPicker.SelectedItem = "AM";
			toTimePicker.SelectedItem = PubCloseTime;
		}

		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		locationComboBox.SelectedIndex = 0;

		await LoadTransactions();
	}

	private async Task LoadTransactions()
	{
		if (locationComboBox.SelectedValue is null ||
			toDatePicker.SelectedDate is null ||
			fromDatePicker.SelectedDate is null ||
			toTimePicker is null ||
			fromTimePicker is null ||
			toSlotPicker is null ||
			fromSlotPicker is null) return;

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;

		var fromTime = fromSlotPicker.SelectedItem.ToString() == "AM" ? (int)fromTimePicker.SelectedItem : (int)fromTimePicker.SelectedItem + 12;
		var toTime = toSlotPicker.SelectedItem.ToString() == "AM" ? (int)toTimePicker.SelectedItem : (int)toTimePicker.SelectedItem + 12;

		transactionDataGrid.ItemsSource = await TransactionData.LoadTransactionsByDateLocation(fromDatePicker.SelectedDate.Value.AddHours(fromTime), toDatePicker.SelectedDate.Value.AddHours(toTime), (int)locationComboBox.SelectedValue);
		foreach (DataGridColumn column in transactionDataGrid.Columns)
		{
			if (new[] { 1, 2 }.Contains(column.DisplayIndex))
				column.Visibility = Visibility.Collapsed;

			if (new[] { 0, 7, 8, 9, 10, 11, 12 }.Contains(column.DisplayIndex))
				column.CellStyle = new Style(typeof(DataGridCell)) { Setters = { new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right) } };
		}
	}

	private async void values_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadTransactions();

	private void transactionDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

	private void UpdateFields()
	{
		if (transactionDataGrid.SelectedItem is TransactionPrintModel selectedTransaction) slipIdTextBox.Text = selectedTransaction.Id.ToString();
		else slipIdTextBox.Clear();
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(slipIdTextBox.Text);

	private async void loadButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Transaction Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		var transaction = await CommonData.LoadTableDataById<TransactionModel>(TableNames.Transaction, int.Parse(slipIdTextBox.Text));

		if (transaction is null)
		{
			MessageBox.Show("Invalid Transaction Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		_parentFrame.Content = new UpdateTransactionPage(transaction, _parentFrame);
	}
}
