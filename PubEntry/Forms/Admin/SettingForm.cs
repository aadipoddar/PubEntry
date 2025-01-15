
namespace PubEntry.Forms.Admin;

public partial class SettingForm : Form
{
	public SettingForm() => InitializeComponent();

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

		var settings = (await CommonData.LoadTableData<SettingsModel>(Table.Settings)).FirstOrDefault();

		inactivityTimeTextBox.Text = settings.InactivityTime.ToString();

		headerFontFamilyComboBox.SelectedValue = settings.HeaderFontFamilyThermal;
		headerFontSizeTextBox.Text = settings.HeaderFontSizeThermal.ToString();
		headerFontStyleComboBox.SelectedValue = settings.HeaderFontStyleThermal;

		subHeaderFontFamilyComboBox.SelectedValue = settings.SubHeaderFontFamilyThermal;
		subHeaderFontSizeTextBox.Text = settings.SubHeaderFontSizeThermal.ToString();
		subHeaderFontStyleComboBox.SelectedValue = settings.SubHeaderFontStyleThermal;

		regularFontFamilyComboBox.SelectedValue = settings.RegularFontFamilyThermal;
		regularFontSizeTextBox.Text = settings.RegularFontSizeThermal.ToString();
		regularFontStyleComboBox.SelectedValue = settings.RegularFontStyleThermal;

		footerFontFamilyComboBox.SelectedValue = settings.FooterFontFamilyThermal;
		footerFontSizeTextBox.Text = settings.FooterFontSizeThermal.ToString();
		footerFontStyleComboBox.SelectedValue = settings.FooterFontStyleThermal;
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

		await SettingsData.UpdateSettings(new SettingsModel
		{
			Id = 1,

			InactivityTime = int.Parse(inactivityTimeTextBox.Text),

			HeaderFontFamilyThermal = headerFontFamilyComboBox.SelectedValue.ToString(),
			HeaderFontSizeThermal = int.Parse(headerFontSizeTextBox.Text),
			HeaderFontStyleThermal = int.Parse(headerFontStyleComboBox.SelectedValue.ToString()),

			SubHeaderFontFamilyThermal = subHeaderFontFamilyComboBox.SelectedValue.ToString(),
			SubHeaderFontSizeThermal = int.Parse(subHeaderFontSizeTextBox.Text),
			SubHeaderFontStyleThermal = int.Parse(subHeaderFontStyleComboBox.SelectedValue.ToString()),

			RegularFontFamilyThermal = regularFontFamilyComboBox.SelectedValue.ToString(),
			RegularFontSizeThermal = int.Parse(regularFontSizeTextBox.Text),
			RegularFontStyleThermal = int.Parse(regularFontStyleComboBox.SelectedValue.ToString()),

			FooterFontFamilyThermal = footerFontFamilyComboBox.SelectedValue.ToString(),
			FooterFontSizeThermal = int.Parse(footerFontSizeTextBox.Text),
			FooterFontStyleThermal = int.Parse(footerFontStyleComboBox.SelectedValue.ToString())
		});

		Close();
	}
}
