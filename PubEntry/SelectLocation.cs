﻿using System;
using System.Drawing.Printing;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntry;

public partial class SelectLocation : Form
{
	public SelectLocation()
	{
		InitializeComponent();

		Task task = LoadComboBox();
	}

	public async Task LoadComboBox()
	{
		locationComboBox.DataSource = null;
		employeeComboBox.DataSource = null;

		var locations = (await DataAccess.LoadTableData<LocationModel>("LocationTable")).ToList();
		var employees = (await DataAccess.LoadTableData<EmployeeModel>("EmployeeTable")).ToList();

		locationComboBox.DataSource = locations;
		locationComboBox.DisplayMember = "Name";

		employeeComboBox.DataSource = employees;
		employeeComboBox.DisplayMember = "Name";
	}

	private void goButton_Click(object sender, EventArgs e)
	{
		var employeeId = employeeComboBox.SelectedIndex;
		if (Task.Run(async () => await DataAccess.GetEmployeePasswordById(employeeId)).Result == passwordTextBox.Text)
		{
			MainForm mainForm = new(locationComboBox.SelectedIndex, employeeComboBox.SelectedIndex);
			mainForm.Show();
			Hide();
		}

		else
			MessageBox.Show("Incorrect Password");
	}

	private void GetDateTime(out string fromDateTime, out string toDateTime)
	{
		fromDateTime = fromDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		toDateTime = toDateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

		fromDateTime = fromDateTime + $" {fromTimeTextBox.Text}:00:00";
		toDateTime = toDateTime + $" {toTimeTextBox.Text}:00:00";
	}

	private string GetFormatedDate(bool getFromDate = true)
	{
		if (getFromDate)
			return fromDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {fromTimeTextBox.Text}:00";

		else
			return toDateTimePicker.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + $" {toTimeTextBox.Text}:00";
	}

	private List<TransactionModel> GetTransactionsByLocationId(int locationId)
	{
		string fromDateTime, toDateTime;
		GetDateTime(out fromDateTime, out toDateTime);
		return Task.Run(async () => await DataAccess.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}

	private void finalPrintButton_Click(object sender, EventArgs e)
	{
		PrintDialog printDialog = new PrintDialog();
		printDialog.Document = printDocument;

		if (printDialog.ShowDialog() == DialogResult.OK)
			printDocument.Print();
	}

	private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
	{
		Graphics g = e.Graphics;
		Font font = new Font("Courier New", 9);

		int y = 0;
		int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0;
		var locations = Task.Run(async () => await DataAccess.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		g.DrawString($"{GetFormatedDate()} - {GetFormatedDate(false)}", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 250, y += 20);

		foreach (var location in locations)
		{
			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0;
			List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

			g.DrawString($"** {location.Name} **", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 350, y += 20);

			g.DrawString("---------------------------------", font, Brushes.Black, 300, y += 15);

			foreach (var transaction in transactions)
			{
				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
			}

			g.DrawString($"Total Persons: {totalMale + totalFemale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Total Amount: {totalCash + totalCard + totalUPI}", font, Brushes.Black, 600, y);

			g.DrawString($"Male: {totalMale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Cash: {totalCash}", font, Brushes.Black, 600, y);

			g.DrawString($"Female: {totalFemale}", font, Brushes.Black, 100, y += 15);
			g.DrawString($"Card: {totalCard}", font, Brushes.Black, 600, y);

			g.DrawString($"UPI: {totalUPI}", font, Brushes.Black, 600, y+=15);

			grandTotalMale += totalMale;
			grandTotalFemale += totalFemale;
			grandTotalCash += totalCash;
			grandTotalCard += totalCard;
			grandTotalUPI += totalUPI;
		}

		g.DrawString("** Grand Total **", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, 350, y += 20);

		g.DrawString("---------------------------------", font, Brushes.Black, 300, y += 15);

		g.DrawString($"Grand Total Persons: {grandTotalMale + grandTotalFemale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Grand Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI}", font, Brushes.Black, 600, y);

		g.DrawString($"Male: {grandTotalMale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Cash: {grandTotalCash}", font, Brushes.Black, 600, y);

		g.DrawString($"Female: {grandTotalFemale}", font, Brushes.Black, 100, y += 15);
		g.DrawString($"Card: {grandTotalCard}", font, Brushes.Black, 600, y);

		g.DrawString($"UPI: {grandTotalUPI}", font, Brushes.Black, 600, y+=15);
	}
}
