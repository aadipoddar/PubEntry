using System.Diagnostics;
using System.Globalization;
using System.Reflection;

using PubEntryLibrary.Printing;

namespace PubEntry.Reports;

public partial class SelectDataForm : Form
{
	int locationId;

	public SelectDataForm(int locationId)
	{
		InitializeComponent();

		this.locationId = locationId;
		LoadTextBoxes();
	}

	private void LoadTextBoxes()
	{
		if (DateTime.Now.Hour > 4)
			fromTimeTextBox.Text = (int.Parse(DateTime.Now.Hour.ToString()) - 3).ToString();

		else fromTimeTextBox.Text = int.Parse(DateTime.Now.Hour.ToString()).ToString();

		toTimeTextBox.Text = int.Parse(DateTime.Now.Hour.ToString()).ToString();
		versionLabel.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
	}

	#region Validation
	private void timeTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
			e.Handled = true;
	}

	private void timeTextBox_TextChanged(object sender, EventArgs e)
	{
		if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
			if (int.TryParse(textBox.Text, out int time))
				if (time > 23)
					textBox.Text = "23";
	}

	private bool ValidateTime()
	{
		if (string.IsNullOrEmpty(toTimeTextBox.Text) || string.IsNullOrEmpty(toTimeTextBox.Text)) return false;

		if (fromDateTimePicker.Value.Date > toDateTimePicker.Value.Date) return false;

		if (int.Parse(fromTimeTextBox.Text) > int.Parse(toTimeTextBox.Text))
			if (fromDateTimePicker.Value == toDateTimePicker.Value)
				return false;

		return true;
	}
	#endregion

	#region GetData
	private string GetFromTime()
	{
		string fromDateTime = fromDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		return fromDateTime + $" {fromTimeTextBox.Text}:00:00";
	}

	private string GetToTime()
	{
		string toDateTime = toDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		return toDateTime + $" {toTimeTextBox.Text}:00:00";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimeTextBox.Text}:00";

		else
			return toDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimeTextBox.Text}:00";
	}
	#endregion

	[STAThread]
	private void summaryReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date");
			return;
		}

		LoadingScreen.ShowSplashScreen();

		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromTime();
		string toTime = GetToTime();

		MemoryStream ms = Printing.PrintSummary(dateHeader, fromTime, toTime);
		using (FileStream stream = new FileStream(Path.Combine(Path.GetTempPath(), "SummaryReport.pdf"), FileMode.Create, FileAccess.Write))
		{
			ms.CopyTo(stream);
		}
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\SummaryReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	[STAThread]
	private void detailReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date");
			return;
		}

		DetailDataForm detailDataForm = new(fromDateTimePicker, toDateTimePicker, fromTimeTextBox, toTimeTextBox, locationId);
		detailDataForm.ShowDialog();
	}
}
