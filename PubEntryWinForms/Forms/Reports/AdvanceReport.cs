using System.Diagnostics;
using System.Reflection;

using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

namespace PubEntry.Forms.Reports;

public partial class AdvanceReport : Form
{
	private readonly DateTime _takenOn;
	private readonly int _locationId;

	public AdvanceReport(DateTime takenOn, int locationId)
	{
		_takenOn = takenOn;
		_locationId = locationId;

		InitializeComponent();
	}

	private void AdvanceReport_Load(object sender, EventArgs e) => LoadData();

	private async void LoadData()
	{
		advanceDataGridView.DataSource = await AdvanceData.LoadAdvancesByTakenOnLocation(_takenOn, _locationId);
		foreach (DataGridViewColumn column in advanceDataGridView.Columns)
			if (new[] { 0, 8, 9, 11, 12, 14 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

		totalDataGridView.DataSource = await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(_takenOn, _locationId);
		foreach (DataGridViewColumn column in totalDataGridView.Columns)
		{
			if (column.Index == 0)
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			else column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";
	}

	private async void excelButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await Excel.AdvanceTakeOnExcel(_takenOn, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "AdvanceTakeOnReport.xlsx"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\AdvanceTakeOnReport.xlsx") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	private async void printButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await PDF.AdvanceTakeOn(_takenOn, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "AdvanceTakeOnReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\AdvanceTakeOnReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}
}
