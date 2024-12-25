using System.Diagnostics;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;
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

		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		int totalLoyalty = 0;

		string fromTime = GetFromDateTime();
		string toTime = GetToDateTime();
		List<TransactionModel> transactions = Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromTime, toTime, locationId)).Result;

		dataGridView.Columns.Add("SlipId", "SlipId");
		dataGridView.Columns.Add("Name", "Name");
		dataGridView.Columns.Add("Number", "Number");
		dataGridView.Columns.Add("Loyalty", "Loyalty");
		dataGridView.Columns.Add("Male", "Male");
		dataGridView.Columns.Add("Female", "Female");
		dataGridView.Columns.Add("Cash", "Cash");
		dataGridView.Columns.Add("Card", "Card");
		dataGridView.Columns.Add("UPI", "UPI");
		dataGridView.Columns.Add("Amex", "Amex");
		dataGridView.Columns.Add("Entered By", "Entered By");
		dataGridView.Columns.Add("Approved By", "Approved By");
		dataGridView.Columns.Add("Date Time", "Date Time");

		dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

		foreach (var transaction in transactions)
		{
			var person = Task.Run(async () => await CommonData.LoadTableDataById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
			string employeeName = Task.Run(async () => await CommonData.LoadTableDataById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

			dataGridView.Rows.Add(
					transaction.Id,
					person.Name,
					person.Number,
					person.Loyalty == 1 ? "Y" : "N",
					transaction.Male,
					transaction.Female,
					transaction.Cash,
					transaction.Card,
					transaction.UPI,
					transaction.Amex,
					employeeName,
					transaction.ApprovedBy,
					transaction.DateTime
				);

			totalMale += transaction.Male;
			totalFemale += transaction.Female;
			totalCash += transaction.Cash;
			totalCard += transaction.Card;
			totalUPI += transaction.UPI;
			totalAmex += transaction.Amex;

			if (person.Loyalty == 1) totalLoyalty++;
		}

		totalPeopleLabel.Text = $"{totalMale + totalFemale}";
		maleLabel.Text = $"{totalMale}";
		femaleLabel.Text = $"{totalFemale}";
		totalLoyaltyLabel.Text = $"{totalLoyalty}";
		totalAmountLabel.Text = $"{totalCash + totalCard + totalUPI + totalAmex}";
		cashLabel.Text = $"{totalCash}";
		cardLabel.Text = $"{totalCard}";
		upiLabel.Text = $"{totalUPI}";
		amexLabel.Text = $"{totalAmex}";
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
