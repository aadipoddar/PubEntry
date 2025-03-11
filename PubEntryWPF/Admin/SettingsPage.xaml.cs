using System.Windows.Controls;

namespace PubEntryWPF.Admin;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
	public SettingsPage() => InitializeComponent();

	private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e) => await LoadData();

	private async Task LoadData()
	{
		#region LoadTime

		List<int> hours = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
		List<string> slots = ["AM", "PM"];

		pubOpenTimePicker.ItemsSource = hours;
		pubCloseTimePicker.ItemsSource = hours;
		pubOpenSlotPicker.ItemsSource = slots;
		pubCloseSlotPicker.ItemsSource = slots;

		int pubOpenTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime));
		int pubCloseTime = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime));

		if (pubOpenTime >= 12)
		{
			pubOpenSlotPicker.SelectedItem = "PM";
			if (pubOpenTime > 12) pubOpenTimePicker.SelectedItem = pubOpenTime - 12;
			else pubOpenTimePicker.SelectedItem = pubOpenTime;
		}

		else
		{
			pubOpenSlotPicker.SelectedItem = "AM";
			pubOpenTimePicker.SelectedItem = pubOpenTime;
		}

		if (pubCloseTime >= 12)
		{
			pubCloseSlotPicker.SelectedItem = "PM";
			if (pubCloseTime > 12) pubCloseTimePicker.SelectedItem = pubCloseTime - 12;
			else pubCloseTimePicker.SelectedItem = pubCloseTime;
		}
		else
		{
			pubCloseSlotPicker.SelectedItem = "AM";
			pubCloseTimePicker.SelectedItem = pubCloseTime;
		}

		#endregion

		inactivityTimerTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.InactivityTime);
	}
}
