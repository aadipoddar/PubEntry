using System.Data;
using System.Diagnostics;
using System.Globalization;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using Syncfusion.Pdf;

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
		return Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromDateTime, toDateTime, locationId)).Result;
	}
	#endregion

	private void LoadComponents()
	{
		dateLabel.Text = $"{GetFormatedDate()} - {GetFormatedDate(false)}";
		locationNameLabel.Text = $"{Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name}";

		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);

		dataGridView.Columns.Add("Id", "Id");
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
		dataGridView.Columns.Add("Date Time", "Date Time");

		dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

		int i = 1;
		foreach (var transaction in transactions)
		{
			var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
			string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

			dataGridView.Rows.Add(
					i,
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
					transaction.DateTime
				);

			totalMale += transaction.Male;
			totalFemale += transaction.Female;
			totalCash += transaction.Cash;
			totalCard += transaction.Card;
			totalUPI += transaction.UPI;
			totalAmex += transaction.Amex;

			i++;
		}

		totalPeopleLabel.Text = $"{totalMale + totalFemale}";
		maleLabel.Text = $"{totalMale}";
		femaleLabel.Text = $"{totalFemale}";
		totalAmountLabel.Text = $"{totalCash + totalCard + totalUPI + totalAmex}";
		cashLabel.Text = $"{totalCash}";
		cardLabel.Text = $"{totalCard}";
		upiLabel.Text = $"{totalUPI}";
		amexLabel.Text = $"{totalAmex}";
	}

	#region Printing
	private void printButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();
		DetailedReport();
		LoadingScreen.CloseForm();
	}

	public PdfPageTemplateElement AddHeader(PdfDocument doc, string title, string description)
	{
		RectangleF rect = new(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement header = new(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
		float doubleHeight = font.Height * 2;
		Color activeColor = Color.FromArgb(44, 71, 120);
		SizeF imageSize = new(110f, 35f);

		PdfSolidBrush brush = new(activeColor);

		PdfPen pen = new(Color.DarkBlue, 3f);
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

		PdfStringFormat format = new();
		format.Alignment = PdfTextAlignment.Center;
		format.LineAlignment = PdfVerticalAlignment.Middle;

		header.Graphics.DrawString(title, font, brush, new RectangleF(0, 0, header.Width, header.Height), format);
		brush = new PdfSolidBrush(Color.Gray);
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 6, PdfFontStyle.Bold);

		format = new PdfStringFormat();
		format.Alignment = PdfTextAlignment.Left;
		format.LineAlignment = PdfVerticalAlignment.Bottom;

		header.Graphics.DrawString(description, font, brush, new RectangleF(0, 0, header.Width, header.Height - 8), format);

		pen = new PdfPen(Color.DarkBlue, 0.7f);
		header.Graphics.DrawLine(pen, 0, 0, header.Width, 0);
		pen = new PdfPen(Color.DarkBlue, 2f);
		header.Graphics.DrawLine(pen, 0, 03, header.Width + 3, 03);
		pen = new PdfPen(Color.DarkBlue, 2f);
		header.Graphics.DrawLine(pen, 0, header.Height - 3, header.Width, header.Height - 3);
		header.Graphics.DrawLine(pen, 0, header.Height, header.Width, header.Height);

		return header;
	}

	public PdfPageTemplateElement AddFooter(PdfDocument doc)
	{
		RectangleF rect = new(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement footer = new(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);

		PdfSolidBrush brush = new(Color.Black);

		PdfPageNumberField pageNumber = new(font, brush);

		PdfPageCountField count = new(font, brush);

		PdfCompositeField compositeField = new(font, brush, "Page {0} of {1}", pageNumber, count);
		compositeField.Bounds = footer.Bounds;

		compositeField.Draw(footer.Graphics, new PointF(470, 40));

		return footer;
	}

	private void DetailedReport()
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, $"{GetFormatedDate()} - {GetFormatedDate(false)}", "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		string text = $"{Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		textElement = new PdfTextElement(text, font);

		if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);

		else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		PdfGrid pdfGrid = new();

		DataTable dataTable = new();

		dataTable.Columns.Add("Id", typeof(int));
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Number", typeof(string));
		dataTable.Columns.Add("Loyalty", typeof(string));
		dataTable.Columns.Add("Male", typeof(int));
		dataTable.Columns.Add("Female", typeof(int));
		dataTable.Columns.Add("Cash", typeof(int));
		dataTable.Columns.Add("Card", typeof(int));
		dataTable.Columns.Add("UPI", typeof(int));
		dataTable.Columns.Add("Amex", typeof(int));
		dataTable.Columns.Add("Entered By", typeof(string));
		dataTable.Columns.Add("Date Time", typeof(DateTime));

		int i = 1;
		foreach (var transaction in transactions)
		{
			var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
			string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

			dataTable.Rows.Add(i, $"{person.Name}", $"{person.Number}", person.Loyalty == 1 ? "Y" : "N", transaction.Male, transaction.Female, transaction.Cash, transaction.Card, transaction.UPI, transaction.Amex, $"{employeeName}", $"{transaction.DateTime}");

			totalMale += transaction.Male;
			totalFemale += transaction.Female;
			totalCash += transaction.Cash;
			totalCard += transaction.Card;
			totalUPI += transaction.UPI;
			totalAmex += transaction.Amex;

			i++;
		}

		pdfGrid.DataSource = dataTable;

		pdfGrid.Columns[0].Width = 20;
		pdfGrid.Columns[2].Width = 60;
		pdfGrid.Columns[3].Width = 30;
		pdfGrid.Columns[4].Width = 30;
		pdfGrid.Columns[5].Width = 30;
		pdfGrid.Columns[9].Width = 30;
		pdfGrid.Columns[10].Width = 40;

		foreach (PdfGridRow row in pdfGrid.Rows)
		{
			foreach (PdfGridCell cell in row.Cells)
			{
				PdfGridCellStyle cellStyle = new()
				{
					CellPadding = new PdfPaddings(5, 5, 5, 5)
				};
				cell.Style = cellStyle;
			}
		}

		pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

		foreach (PdfGridColumn column in pdfGrid.Columns)
			column.Format = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

		result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {totalMale + totalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {totalCash + totalCard + totalUPI + totalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {totalMale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {totalCash}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {totalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {totalCard}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"UPI: {totalUPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Amex: {totalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		pdfDocument.Save($"{Path.GetTempPath()}\\Table.pdf");
		pdfDocument.Close(true);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\Table.pdf") { UseShellExecute = true });
	}
	#endregion

	#region Excel
	private void excelButton_Click(object sender, EventArgs e)
	{
		LoadingScreen.ShowSplashScreen();
		ExportToExcel();
		LoadingScreen.CloseForm();
	}

	private void ExportToExcel()
	{
		List<string> lines = new();
		lines.Add("Id, Name, Number, Loyalty, Male, Female, Cash, Card, UPI, Amex, Entered By, Date Time");

		List<TransactionModel> transactions = GetTransactionsByLocationId(locationId);
		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		int i = 1;

		foreach (var transaction in transactions)
		{
			var person = Task.Run(async () => await CommonData.GetById<PersonModel>("PersonTable", transaction.PersonId)).Result.FirstOrDefault();
			string employeeName = Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name;

			var loyalty = person.Loyalty == 1 ? "Y" : "N";
			lines.Add($"{i}, {person.Name}, {person.Number}, {loyalty}, {transaction.Male}, {transaction.Female}, {transaction.Cash}, {transaction.Card}, {transaction.UPI}, {transaction.Amex}, {employeeName}, {transaction.DateTime}");

			totalMale += transaction.Male;
			totalFemale += transaction.Female;
			totalCash += transaction.Cash;
			totalCard += transaction.Card;
			totalUPI += transaction.UPI;
			totalAmex += transaction.Amex;

			i++;
		}

		lines.Add("");
		lines.Add($",Total People = , {totalMale + totalFemale}, , , , , , Total Amount = , {totalCash + totalCard + totalUPI + totalAmex}");
		lines.Add($",Male = , {totalMale}, , , , , , Cash = , {totalCash}");
		lines.Add($",Female = , {totalFemale}, , , , , , Card = , {totalCard}");
		lines.Add($", , , , , , , , UPI = , {totalUPI}");
		lines.Add($", , , , , , , , Amex = , {totalAmex}");

		File.WriteAllLinesAsync($"{Path.GetTempPath()}\\Table.csv", lines);
		Process.Start(new ProcessStartInfo($"{Path.GetTempPath()}\\Table.csv") { UseShellExecute = true });
	}
	#endregion
}
