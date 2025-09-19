using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PubEntry.Admin;

/// <summary>
/// Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : Page
{
	public SettingsPage() => InitializeComponent();

	private async void Page_Loaded(object sender, RoutedEventArgs e) => await LoadData();

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

		#region Timers
		inactivityTimerTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.InactivityTime);
		refreshReportTimerTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RefreshReportTimer);
		#endregion

		#region Background Service
		backgroundServiceTimerTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceTimer);
		backgroundServiceLocationMarkTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceLocationMark);
		backgroundServiceGrandTotalMarkTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.BackgroundServiceGrandTotalMark);
		#endregion

		#region Thermal

		#region PageSettings

		pageWidthThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.PageWidthThermal);
		pagePaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.PagePaddingTopThermal);
		pagePaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.PagePaddingBottomThermal);
		pagePaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.PagePaddingLeftThermal);
		pagePaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.PagePaddingRightThermal);

		#endregion

		#region HeaderFont

		headerFontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
		headerFontWeightComboBox.ItemsSource = typeof(FontWeights)
							.GetProperties(BindingFlags.Public | BindingFlags.Static)
							.Where(p => p.PropertyType == typeof(FontWeight))
							.Select(p => new
							{
								FontWeight = (FontWeight)p.GetValue(null),
								DisplayName = p.Name
							}).ToList();

		headerFontWeightComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontWeightThermal);
		headerFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontFamilyThermal);

		headerFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontSizeThermal);
		headerFontPaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontPaddingTopThermal);
		headerFontPaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontPaddingBottomThermal);
		headerFontPaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontPaddingLeftThermal);
		headerFontPaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontPaddingRightThermal);

		#endregion

		#region SubHeaderFont

		subHeaderFontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
		subHeaderFontWeightComboBox.ItemsSource = typeof(FontWeights)
							.GetProperties(BindingFlags.Public | BindingFlags.Static)
							.Where(p => p.PropertyType == typeof(FontWeight))
							.Select(p => new
							{
								FontWeight = (FontWeight)p.GetValue(null),
								DisplayName = p.Name
							}).ToList();

		subHeaderFontWeightComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontWeightThermal);
		subHeaderFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontFamilyThermal);

		subHeaderFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontSizeThermal);
		subHeaderFontPaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontPaddingTopThermal);
		subHeaderFontPaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontPaddingBottomThermal);
		subHeaderFontPaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontPaddingLeftThermal);
		subHeaderFontPaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontPaddingRightThermal);

		#endregion

		#region RegularFont

		regularFontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
		regularFontWeightComboBox.ItemsSource = typeof(FontWeights)
							.GetProperties(BindingFlags.Public | BindingFlags.Static)
							.Where(p => p.PropertyType == typeof(FontWeight))
							.Select(p => new
							{
								FontWeight = (FontWeight)p.GetValue(null),
								DisplayName = p.Name
							}).ToList();

		regularFontWeightComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontWeightThermal);
		regularFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontFamilyThermal);

		regularFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontSizeThermal);
		regularFontPaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontPaddingTopThermal);
		regularFontPaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontPaddingBottomThermal);
		regularFontPaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontPaddingLeftThermal);
		regularFontPaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontPaddingRightThermal);

		#endregion

		#region FooterFont

		footerFontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
		footerFontWeightComboBox.ItemsSource = typeof(FontWeights)
							.GetProperties(BindingFlags.Public | BindingFlags.Static)
							.Where(p => p.PropertyType == typeof(FontWeight))
							.Select(p => new
							{
								FontWeight = (FontWeight)p.GetValue(null),
								DisplayName = p.Name
							}).ToList();

		footerFontWeightComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontWeightThermal);
		footerFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontFamilyThermal);

		footerFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontSizeThermal);
		footerFontPaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontPaddingTopThermal);
		footerFontPaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontPaddingBottomThermal);
		footerFontPaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontPaddingLeftThermal);
		footerFontPaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontPaddingRightThermal);

		#endregion

		#region SeparatorFont

		separatorFontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
		separatorFontWeightComboBox.ItemsSource = typeof(FontWeights)
							.GetProperties(BindingFlags.Public | BindingFlags.Static)
							.Where(p => p.PropertyType == typeof(FontWeight))
							.Select(p => new
							{
								FontWeight = (FontWeight)p.GetValue(null),
								DisplayName = p.Name
							}).ToList();

		separatorFontWeightComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontWeightThermal);
		separatorFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontFamilyThermal);
		separatorFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontSizeThermal);

		separatorDashCountTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorDashCountThermal);

		separatorFontPaddingTopThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontPaddingTopThermal);
		separatorFontPaddingBottomThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontPaddingBottomThermal);
		separatorFontPaddingLeftThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontPaddingLeftThermal);
		separatorFontPaddingRightThermalTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SeparatorFontPaddingRightThermal);

		#endregion

		#region FooterLines

		footerLine1TextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterLine1);
		footerLine2TextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterLine2);

		#endregion

		#endregion
	}

	#region Validation

	private void textBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
	{
		Regex regex = new("[^0-9]+");
		e.Handled = regex.IsMatch(e.Text);
	}

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(inactivityTimerTextBox.Text)) inactivityTimerTextBox.Text = "5";
		if (string.IsNullOrEmpty(refreshReportTimerTextBox.Text)) refreshReportTimerTextBox.Text = "60";

		if (string.IsNullOrEmpty(backgroundServiceTimerTextBox.Text)) backgroundServiceTimerTextBox.Text = "5";
		if (string.IsNullOrEmpty(backgroundServiceLocationMarkTextBox.Text)) backgroundServiceLocationMarkTextBox.Text = "25000";
		if (string.IsNullOrEmpty(backgroundServiceGrandTotalMarkTextBox.Text)) backgroundServiceGrandTotalMarkTextBox.Text = "50000";

		return true;
	}

	#endregion

	private async void saveButton_Click(object sender, RoutedEventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please fill all the required fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		List<SettingsModel> settings =
			[
				new SettingsModel { Key = SettingsKeys.PubOpenTime, Value = (pubOpenSlotPicker.SelectedItem.ToString() == "AM" ? (int)pubOpenTimePicker.SelectedItem : (int)pubOpenTimePicker.SelectedItem + 12).ToString()},
				new SettingsModel { Key = SettingsKeys.PubCloseTime, Value = (pubCloseSlotPicker.SelectedItem.ToString() == "AM" ? (int)pubCloseTimePicker.SelectedItem : (int)pubCloseTimePicker.SelectedItem + 12).ToString()},

				new SettingsModel { Key = SettingsKeys.InactivityTime, Value = inactivityTimerTextBox.Text},
				new SettingsModel { Key = SettingsKeys.RefreshReportTimer, Value = refreshReportTimerTextBox.Text},

				new SettingsModel { Key = SettingsKeys.BackgroundServiceTimer, Value = backgroundServiceTimerTextBox.Text},
				new SettingsModel { Key = SettingsKeys.BackgroundServiceLocationMark, Value = backgroundServiceLocationMarkTextBox.Text},
				new SettingsModel { Key = SettingsKeys.BackgroundServiceGrandTotalMark, Value = backgroundServiceGrandTotalMarkTextBox.Text},

				new SettingsModel { Key = SettingsKeys.PageWidthThermal, Value = pageWidthThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.PagePaddingTopThermal, Value = pagePaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.PagePaddingBottomThermal, Value = pagePaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.PagePaddingLeftThermal, Value = pagePaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.PagePaddingRightThermal, Value = pagePaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.HeaderFontFamilyThermal, Value = headerFontFamilyComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.HeaderFontSizeThermal, Value = headerFontSizeTextBox.Text},
				new SettingsModel { Key = SettingsKeys.HeaderFontWeightThermal, Value = headerFontWeightComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.HeaderFontPaddingTopThermal, Value = headerFontPaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.HeaderFontPaddingBottomThermal, Value = headerFontPaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.HeaderFontPaddingLeftThermal, Value = headerFontPaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.HeaderFontPaddingRightThermal, Value = headerFontPaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.SubHeaderFontFamilyThermal, Value = subHeaderFontFamilyComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontSizeThermal, Value = subHeaderFontSizeTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontWeightThermal, Value = subHeaderFontWeightComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontPaddingTopThermal, Value = subHeaderFontPaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontPaddingBottomThermal, Value = subHeaderFontPaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontPaddingLeftThermal, Value = subHeaderFontPaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SubHeaderFontPaddingRightThermal, Value = subHeaderFontPaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.RegularFontFamilyThermal, Value = regularFontFamilyComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.RegularFontSizeThermal, Value = regularFontSizeTextBox.Text},
				new SettingsModel { Key = SettingsKeys.RegularFontWeightThermal, Value = regularFontWeightComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.RegularFontPaddingTopThermal, Value = regularFontPaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.RegularFontPaddingBottomThermal, Value = regularFontPaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.RegularFontPaddingLeftThermal, Value = regularFontPaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.RegularFontPaddingRightThermal, Value = regularFontPaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.FooterFontFamilyThermal, Value = footerFontFamilyComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.FooterFontSizeThermal, Value = footerFontSizeTextBox.Text},
				new SettingsModel { Key = SettingsKeys.FooterFontWeightThermal, Value = footerFontWeightComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.FooterFontPaddingTopThermal, Value = footerFontPaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.FooterFontPaddingBottomThermal, Value = footerFontPaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.FooterFontPaddingLeftThermal, Value = footerFontPaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.FooterFontPaddingRightThermal, Value = footerFontPaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.SeparatorFontFamilyThermal, Value = separatorFontFamilyComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.SeparatorFontSizeThermal, Value = separatorFontSizeTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SeparatorFontWeightThermal, Value = separatorFontWeightComboBox.SelectedValue.ToString()},
				new SettingsModel { Key = SettingsKeys.SeparatorDashCountThermal, Value = separatorDashCountTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SeparatorFontPaddingTopThermal, Value = separatorFontPaddingTopThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SeparatorFontPaddingBottomThermal, Value = separatorFontPaddingBottomThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SeparatorFontPaddingLeftThermal, Value = separatorFontPaddingLeftThermalTextBox.Text},
				new SettingsModel { Key = SettingsKeys.SeparatorFontPaddingRightThermal, Value = separatorFontPaddingRightThermalTextBox.Text},

				new SettingsModel { Key = SettingsKeys.FooterLine1, Value = footerLine1TextBox.Text},
				new SettingsModel { Key = SettingsKeys.FooterLine2, Value = footerLine2TextBox.Text}
			];

		foreach (var setting in settings) await SettingsData.UpdateSettings(setting);

		MessageBox.Show("Settings Saved. Please Restart all Applications.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

		await LoadData();
	}

	private async void resetDefaultButton_Click(object sender, RoutedEventArgs e)
	{
		await SettingsData.ResetSettings();

		MessageBox.Show("Settings Saved. Please Restart all Applications.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

		await LoadData();
	}
}
