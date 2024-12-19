using System.Data;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;

namespace PubEntryLibrary.Printing;

public static class Printing
{
	public static PdfPageTemplateElement AddHeader(PdfDocument doc, string title, string description)
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

	public static PdfPageTemplateElement AddFooter(PdfDocument doc)
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

	public static PdfDocument PrintSummary(string dateHeader, string fromTime, string toTime)
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, dateHeader, "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		string text;
		float textWidth, pageWidth, textX;
		int grandTotalMale = 0, grandTotalFemale = 0, grandTotalCash = 0, grandTotalCard = 0, grandTotalUPI = 0, grandTotalAmex = 0;
		var locations = Task.Run(async () => await CommonData.LoadTableData<LocationModel>("LocationTable")).Result.ToList();

		foreach (var location in locations)
		{
			int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;

			List<TransactionModel> transactions = Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromTime, toTime, location.Id)).Result;

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

		return pdfDocument;
	}

	public static PdfDocument PrintDetail(string dateHeader, string fromTime, string toTime, int selectedLocationId)
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, dateHeader, "Summary Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		int totalMale = 0, totalFemale = 0, totalCash = 0, totalCard = 0, totalUPI = 0, totalAmex = 0;
		int totalLoyalty = 0;

		List<TransactionModel> transactions = Task.Run(async () => await TransactionData.GetTransactionsByDateRangeAndLocation(fromTime, toTime, selectedLocationId)).Result;

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		string text = $"{Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", selectedLocationId)).Result.FirstOrDefault().Name}";
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

			if (person.Loyalty == 1) totalLoyalty++;

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

		textElement = new PdfTextElement($"Total Loyalty: {totalLoyalty}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"UPI: {totalUPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"Amex: {totalAmex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		return pdfDocument;
	}
}
