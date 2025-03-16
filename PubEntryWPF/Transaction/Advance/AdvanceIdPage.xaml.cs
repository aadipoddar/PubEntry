using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PubEntryWPF.Transaction.Advance;

/// <summary>
/// Interaction logic for AdvanceId.xaml
/// </summary>
public partial class AdvanceIdPage : Page
{
	private readonly Frame _parentFrame;

	public AdvanceIdPage(Frame parentFrame)
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
		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			toDatePicker.SelectedDate = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDatePicker.SelectedDate = DateTime.Now.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}

		locationComboBox.ItemsSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMemberPath = nameof(LocationModel.Name);
		locationComboBox.SelectedValuePath = nameof(LocationModel.Id);
		locationComboBox.SelectedIndex = 0;

		await LoadAdvances();
	}

	private async Task LoadAdvances()
	{
		if (fromDatePicker.SelectedDate is null || toDatePicker.SelectedDate is null || locationComboBox.SelectedValue is null) return;

		fromDatePicker.DisplayDateEnd = toDatePicker.SelectedDate;
		toDatePicker.DisplayDateStart = fromDatePicker.SelectedDate;

		advanceDataGrid.ItemsSource = await AdvanceData.LoadAdvancesByForDateLocation(fromDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value, (int)locationComboBox.SelectedValue);
		foreach (DataGridColumn column in advanceDataGrid.Columns)
			if (new[] { 0, 8, 9, 11, 12, 14 }.Contains(column.DisplayIndex))
				column.CellStyle = new Style(typeof(DataGridCell)) { Setters = { new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right) } };
	}

	private async void values_SelectionChanged(object sender, SelectionChangedEventArgs e) => await LoadAdvances();

	private void advanceDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) => UpdateFields();

	private void UpdateFields()
	{
		if (advanceDataGrid.SelectedItem is AdvancePrintModel selectedAdvance)
		{
			advanceIdTextBox.Text = selectedAdvance.Id.ToString();

			if (selectedAdvance.SlipId != "NOT REDEEMED")
				loadButton.Content = "Load Corresponding Transaction";

			else loadButton.Content = "Load Advance";
		}

		else
		{
			advanceIdTextBox.Clear();
			loadButton.Content = "Load Advance";
		}
	}

	private bool ValidateForm() => !string.IsNullOrEmpty(advanceIdTextBox.Text);

	private async void advanceIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!ValidateForm())
		{
			loadButton.Content = "Load Advance";
			return;
		}
		var advance = await CommonData.LoadTableDataById<AdvanceModel>(TableNames.Advance, int.Parse(advanceIdTextBox.Text));
		if (advance.TransactionId != 0) loadButton.Content = "Load Corresponding Transaction";
		else loadButton.Content = "Load Advance";
	}

	private async void loadButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		var advance = await CommonData.LoadTableDataById<AdvanceModel>(TableNames.Advance, int.Parse(advanceIdTextBox.Text));

		if (advance is null)
		{
			MessageBox.Show("Invalid Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		if (advance.TransactionId != 0)
		{
			var transaction = await CommonData.LoadTableDataById<TransactionModel>(TableNames.Transaction, advance.TransactionId);
			_parentFrame.Content = new UpdateTransactionPage(transaction, _parentFrame);
		}

		else _parentFrame.Content = new UpdateAdvancePage(advance, _parentFrame);
	}
}
