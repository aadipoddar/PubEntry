using System.Diagnostics;
using System.Reflection;

using PubEntryLibrary.Printing;

namespace PubEntry.Forms.Reports;

public partial class SelectDataForm : Form
{
	public SelectDataForm() => InitializeComponent();

	private async void SelectDataForm_Load(object sender, EventArgs e) => await LoadData();

	private async Task LoadData()
	{
		locationComboBox.DataSource = await CommonData.LoadTableDataByStatus<LocationModel>("LocationTable", true);
		locationComboBox.DisplayMember = nameof(LocationModel.Name);
		locationComboBox.ValueMember = nameof(LocationModel.Id);

		if (DateTime.Now.Hour >= 17)
		{
			toDateTimePicker.Value = DateTime.Now.Date.AddDays(1).AddHours(5);
			fromDateTimePicker.Value = DateTime.Now.Date.AddHours(17);
		}
		else
		{
			toDateTimePicker.Value = DateTime.Now.Date.AddHours(5);
			fromDateTimePicker.Value = DateTime.Now.Date.AddDays(-1).AddHours(17);
		}

		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
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

		MemoryStream ms = await PrintReport.PrintSummary(fromDateTimePicker.Value, toDateTimePicker.Value);
		using (FileStream stream = new FileStream(Path.Combine(Path.GetTempPath(), "SummaryReport.pdf"), FileMode.Create, FileAccess.Write)) ms.CopyTo(stream);
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
}
