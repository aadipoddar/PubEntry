using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;

using System.Diagnostics;
using System.Globalization;

namespace PubEntry.Reports;

public partial class SelectDataForm : Form
{
	int locationId;

	public SelectDataForm(int locationId)
	{
		InitializeComponent();

		this.locationId = locationId;
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

	[STAThread]
	private void summaryReportButton_Click(object sender, EventArgs e)
	{
		if (!ValidateTime())
		{
			MessageBox.Show("Incorrect Time or Date");
			return;
		}

		LoadingScreen.ShowSplashScreen();
		SummaryReport();
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

	#region Printing
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

	public PdfPageTemplateElement AddHeader(PdfDocument doc, string title, string description)
	{
		RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement header = new PdfPageTemplateElement(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 24);
		float doubleHeight = font.Height * 2;
		Color activeColor = Color.FromArgb(44, 71, 120);
		SizeF imageSize = new SizeF(110f, 35f);

		PdfSolidBrush brush = new PdfSolidBrush(activeColor);

		PdfPen pen = new PdfPen(Color.DarkBlue, 3f);
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);

		PdfStringFormat format = new PdfStringFormat();
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
		RectangleF rect = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);

		PdfPageTemplateElement footer = new PdfPageTemplateElement(rect);
		PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);

		PdfSolidBrush brush = new PdfSolidBrush(Color.Black);

		PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);

		PdfPageCountField count = new PdfPageCountField(font, brush);

		PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, count);
		compositeField.Bounds = footer.Bounds;

		compositeField.Draw(footer.Graphics, new PointF(470, 40));

		return footer;
	}

	private void SummaryReport()
	{
		PdfDocument pdfDocument = new PdfDocument();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, $"{GetFormatedDate()} - {GetFormatedDate(false)}", "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new PdfLayoutFormat();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		string text;
		float textWidth, pageWidth, textX;
		int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0, grandTotalAmex = 0;
		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		foreach (var location in locations)
		{
			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
			List<TransactionModel> transactions = GetTransactionsByLocationId(location.Id);

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

			text = $"{location.Name}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = (pageWidth - textWidth) / 2f;

			textElement = new PdfTextElement(text, font);

			if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);

			else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

			foreach (var transaction in transactions)
			{
				totalMale += transaction.Male;
				totalFemale += transaction.Female;
				totalCash += transaction.Cash;
				totalCard += transaction.Card;
				totalUPI += transaction.UPI;
				totalAmex += transaction.Amex;
			}

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

			grandTotalMale += totalMale;
			grandTotalFemale += totalFemale;
			grandTotalCash += totalCash;
			grandTotalCard += totalCard;
			grandTotalUPI += totalUPI;
			grandTotalAmex += totalAmex;
		}

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);
		text = "Grand Total";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {grandTotalMale + grandTotalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {grandTotalCash + grandTotalCard + grandTotalUPI + grandTotalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {grandTotalMale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {grandTotalCash}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {grandTotalFemale}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {grandTotalCard}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"UPI: {grandTotalUPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Amex: {grandTotalAmex}";
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
}
