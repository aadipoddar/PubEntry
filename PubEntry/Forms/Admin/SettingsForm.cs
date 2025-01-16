namespace PubEntry.Forms.Admin;

public partial class SettingsForm : Form
{
	public SettingsForm() => InitializeComponent();

	private async void SettingForm_Load(object sender, EventArgs e) => await LoadData();

	public class FontStyleItem
	{
		public FontStyle FontStyle { get; set; }
		public string DisplayName { get; set; }
		public int Value => (int)FontStyle;
		public override string ToString() => DisplayName;
	}

	private async Task LoadData()
	{
		headerFontFamilyComboBox.DataSource = FontFamily.Families;
		headerFontFamilyComboBox.DisplayMember = "Name";
		headerFontFamilyComboBox.ValueMember = "Name";

		subHeaderFontFamilyComboBox.DataSource = FontFamily.Families;
		subHeaderFontFamilyComboBox.DisplayMember = "Name";
		subHeaderFontFamilyComboBox.ValueMember = "Name";

		regularFontFamilyComboBox.DataSource = FontFamily.Families;
		regularFontFamilyComboBox.DisplayMember = "Name";
		regularFontFamilyComboBox.ValueMember = "Name";

		footerFontFamilyComboBox.DataSource = FontFamily.Families;
		footerFontFamilyComboBox.DisplayMember = "Name";
		footerFontFamilyComboBox.ValueMember = "Name";

		headerFontStyleComboBox.DataSource = Enum.GetValues(typeof(FontStyle)).Cast<FontStyle>().Select(fs => new FontStyleItem { FontStyle = fs, DisplayName = fs.ToString() }).ToList();
		headerFontStyleComboBox.DisplayMember = nameof(FontStyleItem.DisplayName);
		headerFontStyleComboBox.ValueMember = nameof(FontStyleItem.Value);

		subHeaderFontStyleComboBox.DataSource = Enum.GetValues(typeof(FontStyle)).Cast<FontStyle>().Select(fs => new FontStyleItem { FontStyle = fs, DisplayName = fs.ToString() }).ToList();
		subHeaderFontStyleComboBox.DisplayMember = nameof(FontStyleItem.DisplayName);
		subHeaderFontStyleComboBox.ValueMember = nameof(FontStyleItem.Value);

		regularFontStyleComboBox.DataSource = Enum.GetValues(typeof(FontStyle)).Cast<FontStyle>().Select(fs => new FontStyleItem { FontStyle = fs, DisplayName = fs.ToString() }).ToList();
		regularFontStyleComboBox.DisplayMember = nameof(FontStyleItem.DisplayName);
		regularFontStyleComboBox.ValueMember = nameof(FontStyleItem.Value);

		footerFontStyleComboBox.DataSource = Enum.GetValues(typeof(FontStyle)).Cast<FontStyle>().Select(fs => new FontStyleItem { FontStyle = fs, DisplayName = fs.ToString() }).ToList();
		footerFontStyleComboBox.DisplayMember = nameof(FontStyleItem.DisplayName);
		footerFontStyleComboBox.ValueMember = nameof(FontStyleItem.Value);

		pubOpenTimePicker.Value = DateTime.Today.Add(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)));
		pubCloseTimePicker.Value = DateTime.Today.Add(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)));

		inactivityTimeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.InactivityTime);

		headerFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontFamilyThermal);
		headerFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontSizeThermal);
		headerFontStyleComboBox.SelectedValue = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.HeaderFontStyleThermal));

		subHeaderFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontFamilyThermal);
		subHeaderFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontSizeThermal);
		subHeaderFontStyleComboBox.SelectedValue = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.SubHeaderFontStyleThermal));

		regularFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontFamilyThermal);
		regularFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontSizeThermal);
		regularFontStyleComboBox.SelectedValue = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.RegularFontStyleThermal));

		footerFontFamilyComboBox.SelectedValue = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontFamilyThermal);
		footerFontSizeTextBox.Text = await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontSizeThermal);
		footerFontStyleComboBox.SelectedValue = int.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.FooterFontStyleThermal));
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			e.Handled = true;
	}

	private void textBox_Click(object sender, EventArgs e) => (sender as TextBox).SelectAll();

	private bool ValidateForm()
	{
		if (string.IsNullOrEmpty(inactivityTimeTextBox.Text)) inactivityTimeTextBox.Text = "5";

		return true;
	}

	private async void saveButton_Click(object sender, EventArgs e)
	{
		if (!ValidateForm())
		{
			MessageBox.Show("Please all Fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		List<SettingsModel> settings =
		[
			new SettingsModel { Id = 1, Key = SettingsKeys.PubOpenTime, Value = pubOpenTimePicker.Value.TimeOfDay.ToString() },
			new SettingsModel { Id = 2, Key = SettingsKeys.PubCloseTime, Value = pubCloseTimePicker.Value.TimeOfDay.ToString() },

			new SettingsModel { Id = 3, Key = SettingsKeys.InactivityTime, Value = inactivityTimeTextBox.Text },

			new SettingsModel { Id = 4, Key = SettingsKeys.HeaderFontFamilyThermal, Value = headerFontFamilyComboBox.SelectedValue.ToString() },
			new SettingsModel { Id = 5, Key = SettingsKeys.HeaderFontSizeThermal, Value = headerFontSizeTextBox.Text },
			new SettingsModel { Id = 6, Key = SettingsKeys.HeaderFontStyleThermal, Value = headerFontStyleComboBox.SelectedValue.ToString() },

			new SettingsModel { Id = 7, Key = SettingsKeys.SubHeaderFontFamilyThermal, Value = subHeaderFontFamilyComboBox.SelectedValue.ToString() },
			new SettingsModel { Id = 8, Key = SettingsKeys.SubHeaderFontSizeThermal, Value = subHeaderFontSizeTextBox.Text },
			new SettingsModel { Id = 9, Key = SettingsKeys.SubHeaderFontStyleThermal, Value = subHeaderFontStyleComboBox.SelectedValue.ToString() },

			new SettingsModel { Id = 10, Key = SettingsKeys.RegularFontFamilyThermal, Value = regularFontFamilyComboBox.SelectedValue.ToString() },
			new SettingsModel { Id = 11, Key = SettingsKeys.RegularFontSizeThermal, Value = regularFontSizeTextBox.Text },
			new SettingsModel { Id = 12, Key = SettingsKeys.RegularFontStyleThermal, Value = regularFontStyleComboBox.SelectedValue.ToString() },

			new SettingsModel { Id = 13, Key = SettingsKeys.FooterFontFamilyThermal, Value = footerFontFamilyComboBox.SelectedValue.ToString() },
			new SettingsModel { Id = 14, Key = SettingsKeys.FooterFontSizeThermal, Value = footerFontSizeTextBox.Text },
			new SettingsModel { Id = 15, Key = SettingsKeys.FooterFontStyleThermal, Value = footerFontStyleComboBox.SelectedValue.ToString() },
		];

		foreach (var setting in settings) await SettingsData.UpdateSettings(setting);

		Close();
	}
}
