﻿using System.Diagnostics;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;
using PubEntryLibrary.Models.Printing;
using PubEntryLibrary.Printing;

namespace PubEntry.Reports;

public partial class DetailDataForm : Form
{
	DateTimePicker fromDateTimePicker, toDateTimePicker;
	TextBox fromTimeTextBox, toTimeTextBox;
	int locationId;

	public DetailDataForm(DateTimePicker fromDateTimePicker, DateTimePicker toDateTimePicker, TextBox fromTimeTextBox, TextBox toTimeTextBox, int locationId)
	{
		this.fromDateTimePicker = fromDateTimePicker;
		this.toDateTimePicker = toDateTimePicker;
		this.fromTimeTextBox = fromTimeTextBox;
		this.toTimeTextBox = toTimeTextBox;
		this.locationId = locationId;
		AutoScroll = true;

		InitializeComponent();
		RefreshForm();
	}

	private void RefreshForm()
	{
		LoadingScreen.ShowSplashScreen();
		LoadComponents();
		LoadingScreen.CloseForm();
	}

	private void refreshButton_Click(object sender, EventArgs e) => RefreshForm();

	#region GetData
	private string GetFromDateTime()
	{
		string fromDateTime = fromDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		return fromDateTime + $" {fromTimeTextBox.Text}:00:00";
	}

	private string GetToDateTime()
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

	private void LoadComponents()
	{
		dateLabel.Text = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		locationNameLabel.Text = $"{Task.Run(async () => await CommonData.LoadTableDataById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name}";

		List<DetailedPrintModel> detailedPrintModel = Task.Run(async () => await PrintData.LoadDetailedPrint(GetFromDateTime(), GetToDateTime(), locationId)).Result.ToList();

		dataGridView.DataSource = detailedPrintModel;

		dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

		totalPeopleLabel.Text = $"{detailedPrintModel.Sum(x => x.Male) + detailedPrintModel.Sum(x => x.Female)}";
		maleLabel.Text = $"{detailedPrintModel.Sum(x => x.Male)}";
		femaleLabel.Text = $"{detailedPrintModel.Sum(x => x.Female)}";
		totalLoyaltyLabel.Text = $"{detailedPrintModel.Count(x => x.Loyalty == 'L')}";
		totalAmountLabel.Text = $"{detailedPrintModel.Sum(x => x.Cash) + detailedPrintModel.Sum(x => x.Card) + detailedPrintModel.Sum(x => x.UPI) + detailedPrintModel.Sum(x => x.Amex)}";
		cashLabel.Text = $"{detailedPrintModel.Sum(x => x.Cash)}";
		cardLabel.Text = $"{detailedPrintModel.Sum(x => x.Card)}";
		upiLabel.Text = $"{detailedPrintModel.Sum(x => x.UPI)}";
		amexLabel.Text = $"{detailedPrintModel.Sum(x => x.Amex)}";
	}

	private void printButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms = PrintReport.PrintDetail(dateHeader, fromTime, toTime, locationId);
		using (FileStream stream = new FileStream(Path.Combine(Path.GetTempPath(), "DetailedReport.pdf"), FileMode.Create, FileAccess.Write))
		{
			ms.WriteTo(stream);
		}
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReport.pdf") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}

	private void excelButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();

		string dateHeader = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();

		MemoryStream ms = Excel.ExcelExport(dateHeader, fromTime, toTime, locationId);
		using (FileStream stream = new FileStream(Path.Combine(Path.GetTempPath(), "DetailedReportExcel.xlsx"), FileMode.Create, FileAccess.Write))
		{
			ms.WriteTo(stream);
		}
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\DetailedReportExcel.xlsx") { UseShellExecute = true });

		LoadingScreen.CloseForm();
	}
}
