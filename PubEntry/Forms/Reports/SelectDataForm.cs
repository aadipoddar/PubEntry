using System.Diagnostics;
using System.Reflection;

using PubEntryLibrary.Printing.PDF;

namespace PubEntry.Forms.Reports;

public partial class SelectDataForm : Form
{
	public SelectDataForm() => InitializeComponent();

	private async void SelectDataForm_Load(object sender, EventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		if (DateTime.Now.Hour >= TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours)
		{
			toDateTimePicker.Value = DateTime.Now.Date.AddDays(1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDateTimePicker.Value = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}
		else
		{
			toDateTimePicker.Value = DateTime.Now.Date.AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubCloseTime)).Hours);
			fromDateTimePicker.Value = DateTime.Now.Date.AddDays(-1).AddHours(TimeSpan.Parse(await SettingsData.LoadSettingsByKey(SettingsKeys.PubOpenTime)).Hours);
		}

		takeOnDatePicker.Value = DateTime.Now.Date;

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private bool ValidateTime() => fromDateTimePicker.Value < toDateTimePicker.Value;

	[STAThread]
	private async void summaryReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await PDF.Summary(fromDateTimePicker.Value, toDateTimePicker.Value);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "SummaryReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\SummaryReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	[STAThread]
	private void detailReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		DetailDataForm detailDataForm = new(fromDateTimePicker.Value, toDateTimePicker.Value, (locationComboBox.SelectedItem as LocationModel).Id);
		detailDataForm.ShowDialog();
	}

	private void advanceButton_Click(object sender, EventArgs e)
	{
		AdvanceReport advanceReport = new(takeOnDatePicker.Value.Date, (locationComboBox.SelectedItem as LocationModel).Id);
		advanceReport.ShowDialog();
	}
}
