﻿using System.Diagnostics;
using System.Reflection;

using PubEntryLibrary.Printing.Excel;
using PubEntryLibrary.Printing.PDF;

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
		richTextBoxFooter.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

		dateLabel.Text = $"{_fromDateTime} - {_toDateTime}";
		locationNameLabel.Text = $"{(await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, _locationId)).Name}";

		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(_fromDateTime, _toDateTime, _locationId);

		var detailedAdvancePrintModel = _toDateTime.TimeOfDay < TimeSpan.FromHours(17)
			? await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), _locationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(_fromDateTime.Date, _toDateTime.Date, _locationId);

		transactionDataGridView.DataSource = detailedTransactionPrintModel;
		foreach (DataGridViewColumn column in transactionDataGridView.Columns)
		{
			if (new[] { 1, 2 }.Contains(column.Index))
				column.Visible = false;

			if (new[] { 0, 7, 8, 9, 11, 12 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		}

		peopleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Male + x.Female)}";
		maleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Male)}";
		femaleTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Female)}";
		loyaltyTextBox.Text = $"{detailedTransactionPrintModel.Count(x => x?.Loyalty == 'L')}";

		amountTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Cash + x.Card + x.UPI + x.Amex)}";
		cashTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Cash)}";
		cardTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Card)}";
		upiTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.UPI)}";
		amexTextBox.Text = $"{detailedTransactionPrintModel.Sum(x => x?.Amex)}";

		advanceTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x?.Amount)}";
		redeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId != "NOT REDEEMED").Sum(x => x?.Amount)}";
		notRedeemedAdvanceTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId == "NOT REDEEMED").Sum(x => x?.Amount)}";

		bookingTextBox.Text = $"{detailedAdvancePrintModel.Sum(x => x?.Booking)}";
		redeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId != "NOT REDEEMED").Sum(x => x?.Booking)}";
		notRedeemedBookingTextBox.Text = $"{detailedAdvancePrintModel.Where(x => x?.SlipId == "NOT REDEEMED").Sum(x => x?.Booking)}";

		advanceDataGridView.DataSource = detailedAdvancePrintModel;
		foreach (DataGridViewColumn column in advanceDataGridView.Columns)
			if (new[] { 0, 8, 9, 11, 12, 14 }.Contains(column.Index))
				column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
	}

	private void SelectTransactionRows(string slipId)
	{
		transactionDataGridView.ClearSelection();
		foreach (DataGridViewRow row in transactionDataGridView.Rows)
			if (row.Cells["Id"].Value?.ToString() == slipId)
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

	private async void excelButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await Excel.TransactionAdvanceExcel(_fromDateTime, _toDateTime, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReport.xlsx"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.xlsx") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	private async void printButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		MemoryStream ms = await PDF.Detail(_fromDateTime, _toDateTime, _locationId);
		using FileStream stream = new(Path.Combine(Path.GetTempPath(), "DetailedReport.pdf"), FileMode.Create, FileAccess.Write);
		await ms.CopyToAsync(stream);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}
}
