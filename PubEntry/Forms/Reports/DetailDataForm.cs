using System.Diagnostics;

using PubEntryLibrary.Printing;
using PubEntryLibrary.Printing.Excel;

namespace PubEntry.Forms.Reports;

public partial class DetailDataForm : Form
{
	private readonly int _locationId;
	private readonly DateTime _fromDateTime, _toDateTime;

	public DetailDataForm(DateTime fromDateTime, DateTime toDateTime, int locationId)
	{
		_locationId = locationId;
		_fromDateTime = fromDateTime;
		_toDateTime = toDateTime;

		InitializeComponent();
	}

	private void DetailDataForm_Load(object sender, EventArgs e) => LoadComponents();

	private void refreshButton_Click(object sender, EventArgs e) => LoadComponents();

	private async void LoadComponents()
	{
		dateLabel.Text = $"{_fromDateTime} - {_toDateTime}";
		locationNameLabel.Text = $"{(await CommonData.LoadTableDataById<LocationModel>("LocationTable", _locationId)).FirstOrDefault().Name}";

		var detailedTransactionPrintModel = (await PrintData.LoadTransactionsByDateAndLocation(_fromDateTime, _toDateTime, _locationId)).ToList();

		var detailedAdvancePrintModel = _toDateTime.TimeOfDay < TimeSpan.FromHours(17)
			? (await PrintData.LoadAdvancesByTakenForAndLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), _locationId)).ToList()
			: (await PrintData.LoadAdvancesByTakenForAndLocation(_fromDateTime.Date, _toDateTime.Date, _locationId)).ToList();

		transactionDataGridView.DataSource = detailedTransactionPrintModel;
		foreach (DataGridViewColumn column in transactionDataGridView.Columns)
		{
			if (column.Index == 1)
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			else if (new[] { 0, 4, 5, 6, 7, 8, 9 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		peopleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Male + x.Female)}";
		maleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Male)}";
		femaleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Female)}";
		loyaltyTextBox.Text = $"{detailedTransactionPrintModel.Count(x => x.Loyalty == 'L')}";

		amountTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Cash + x.Card + x.UPI + x.Amex)}";
		cashTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Cash)}";
		cardTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Card)}";
		upiTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.UPI)}";
		amexTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x.Amex)}";

		advanceTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x.Adv_Paid)}";
		redeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Adv_Paid)}";
		notRedeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Adv_Paid)}";

		bookingTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x.Booking_Amt)}";
		redeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Booking_Amt)}";
		notRedeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Booking_Amt)}";

		advanceDataGridView.DataSource = detailedAdvancePrintModel;
		foreach (DataGridViewColumn column in advanceDataGridView.Columns)
		{
			if (column.Index == 1)
				column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			else if (new[] { 0, 7, 8, 10, 11, 13 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}
	}

	private void SelectTransactionRows(string slipId)
	{
		transactionDataGridView.ClearSelection();
		foreach (DataGridViewRow row in transactionDataGridView.Rows)
			if (row.Cells["SlipId"].Value?.ToString() == slipId)
			{
				row.Selected = true;
				transactionDataGridView.FirstDisplayedScrollingRowIndex = row.Index;
			}
	}

	private void advanceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			string slipId = advanceDataGridView.Rows[e.RowIndex].Cells["Slip_No"].Value?.ToString();
			if (!string.IsNullOrEmpty(slipId)) SelectTransactionRows(slipId);
		}
	}

	private async void printButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await PrintReport.PrintDetail(_fromDateTime, _toDateTime, _locationId);

		using (FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReport.pdf"), FileMode.Create, FileAccess.Write))
			ms.WriteTo(stream);

		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	private async void excelButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await Excel.ExcelExport(_fromDateTime, _toDateTime, _locationId);

		using (FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReportExcel.xlsx"), FileMode.Create, FileAccess.Write))
			ms.WriteTo(stream);

		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReportExcel.xlsx") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}
}
