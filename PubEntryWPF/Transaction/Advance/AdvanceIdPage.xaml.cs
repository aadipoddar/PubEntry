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

	private bool ValidateForm() => !string.IsNullOrEmpty(advanceIdTextBox.Text);

	private async void loadButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please enter Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		var advance = await CommonData.LoadTableDataById<AdvanceModel>(TableNames.Advance, int.Parse(advanceIdTextBox.Text));

		if (advance is null || advance.TransactionId != 0)
		{
			MessageBox.Show("Invalid Advance Id", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		_parentFrame.Content = new UpdateAdvancePage(advance, _parentFrame);
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

		var advances = await CommonData.LoadTableData<AdvancePrintModel>(ViewNames.Advances);
		advanceDataGrid.ItemsSource = advances;

		//var detailedAdvancePrintModel = _toDateTime.TimeOfDay < TimeSpan.FromHours(17)
		//? await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), _locationId)
		//: await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.Date, _locationId);
	}
}
