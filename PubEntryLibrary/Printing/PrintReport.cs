using PubEntryLibrary.Data;
using PubEntryLibrary.Models.Printing;

using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using Color = Syncfusion.Drawing.Color;
using PointF = Syncfusion.Drawing.PointF;
using SizeF = Syncfusion.Drawing.SizeF;

namespace PubEntryLibrary.Printing;

public static class PrintReport
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

	public static async Task<MemoryStream> PrintSummary(DateTime fromDateTime, DateTime toDateTime)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

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
		List<TransactionTotalsModel> transactionTotalsModel = new();

		string text;
		float textWidth, pageWidth, textX;
		var locations = await LocationData.LoadActiveLocations();

		foreach (var location in locations)
		{
			font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

			text = $"{location.Name}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = (pageWidth - textWidth) / 2f;
			textElement = new PdfTextElement(text, font);
			if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);
			else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

			transactionTotalsModel.Add((await PrintData.LoadTransactionTotals(fromDateTime, toDateTime, location.Id)).FirstOrDefault());

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

			textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Last().TotalMale + transactionTotalsModel.Last().TotalFemale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Total Amount: {transactionTotalsModel.Last().TotalCash + transactionTotalsModel.Last().TotalCard + transactionTotalsModel.Last().TotalUPI + transactionTotalsModel.Last().TotalAmex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Male: {transactionTotalsModel.Last().TotalMale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Cash: {transactionTotalsModel.Last().TotalCash}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Female: {transactionTotalsModel.Last().TotalFemale}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Card: {transactionTotalsModel.Last().TotalCard}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Total Loyalty: {transactionTotalsModel.Last().TotalLoyalty}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"UPI: {transactionTotalsModel.Last().TotalUPI}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			text = $"Amex: {transactionTotalsModel.Last().TotalAmex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);
		}

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);
		text = "Grand Total";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Sum(x => x.TotalMale) + transactionTotalsModel.Sum(x => x.TotalFemale)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {transactionTotalsModel.Sum(x => x.TotalCash) + transactionTotalsModel.Sum(x => x.TotalCard) + transactionTotalsModel.Sum(x => x.TotalUPI) + transactionTotalsModel.Sum(x => x.TotalAmex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {transactionTotalsModel.Sum(x => x.TotalMale)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {transactionTotalsModel.Sum(x => x.TotalCash)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {transactionTotalsModel.Sum(x => x.TotalFemale)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {transactionTotalsModel.Sum(x => x.TotalCard)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Grand Total Loyalty: {transactionTotalsModel.Sum(x => x.TotalLoyalty)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"UPI: {transactionTotalsModel.Sum(x => x.TotalUPI)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"Amex: {transactionTotalsModel.Sum(x => x.TotalAmex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}

	public static async Task<MemoryStream> PrintDetail(DateTime fromDateTime, DateTime toDateTime, int selectedLocationId)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = AddHeader(pdfDocument, dateHeader, "Detail Report");
		pdfDocument.Template.Bottom = AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new();
		layoutFormat.Layout = PdfLayoutType.Paginate;
		layoutFormat.Break = PdfLayoutBreakType.FitPage;

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		string text = $"{(await CommonData.LoadTableDataById<LocationModel>("LocationTable", selectedLocationId)).FirstOrDefault().Name}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		textElement = new PdfTextElement(text, font);

		if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);
		else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);


		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		#region TransactionDetails
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
		text = "Transaction Details";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		PdfGrid pdfGrid = new();

		var detailedTransactionPrintModel = await PrintData.LoadTransactionsByDateAndLocation(fromDateTime, toDateTime, selectedLocationId);

		pdfGrid.DataSource = detailedTransactionPrintModel;

		pdfGrid.Columns[0].Width = 30;
		pdfGrid.Columns[2].Width = 60;
		pdfGrid.Columns[3].Width = 27;
		pdfGrid.Columns[4].Width = 25;
		pdfGrid.Columns[5].Width = 30;
		pdfGrid.Columns[9].Width = 30;
		pdfGrid.Columns[10].Width = 40;

		foreach (PdfGridRow row in pdfGrid.Rows)
		{
			foreach (PdfGridCell cell in row.Cells)
			{
				PdfGridCellStyle cellStyle = new()
				{
					CellPadding = new PdfPaddings(5, 5, 5, 5),
					Font = new PdfStandardFont(PdfFontFamily.Helvetica, 7)
				};
				cell.Style = cellStyle;
			}
		}

		pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

		foreach (PdfGridColumn column in pdfGrid.Columns)
			column.Format = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

		pdfGrid.Columns[1].Format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);

		result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);
		#endregion

		#region AdvanceDetails
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
		text = "Advance Details";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		var detailedAdvancePrintModel = toDateTime.TimeOfDay < TimeSpan.FromHours(17)
			? (await PrintData.LoadAdvancesByTakenForAndLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), selectedLocationId)).ToList()
			: (await PrintData.LoadAdvancesByTakenForAndLocation(fromDateTime.Date, toDateTime.Date, selectedLocationId)).ToList();

		if (detailedAdvancePrintModel.Count() > 0)
		{
			pdfGrid = new();

			pdfGrid.DataSource = detailedAdvancePrintModel;

			pdfGrid.Columns[0].Width = 30;
			pdfGrid.Columns[2].Width = 60;
			pdfGrid.Columns[3].Width = 27;
			pdfGrid.Columns[4].Width = 40;
			pdfGrid.Columns[5].Width = 40;
			pdfGrid.Columns[6].Width = 40;
			pdfGrid.Columns[10].Width = 30;

			foreach (PdfGridRow row in pdfGrid.Rows)
			{
				foreach (PdfGridCell cell in row.Cells)
				{
					PdfGridCellStyle cellStyle = new()
					{
						CellPadding = new PdfPaddings(5, 5, 5, 5),
						Font = new PdfStandardFont(PdfFontFamily.Helvetica, 7)
					};
					cell.Style = cellStyle;
				}
			}

			pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

			foreach (PdfGridColumn column in pdfGrid.Columns)
				column.Format = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

			pdfGrid.Columns[1].Format = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);

			result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);
		}
		#endregion

		#region Totals
		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		textElement = new PdfTextElement($"Total People: {detailedTransactionPrintModel.Sum(x => x.Male) + detailedTransactionPrintModel.Sum(x => x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {detailedTransactionPrintModel.Sum(x => x.Cash) + detailedTransactionPrintModel.Sum(x => x.Card) + detailedTransactionPrintModel.Sum(x => x.UPI) + detailedTransactionPrintModel.Sum(x => x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {detailedTransactionPrintModel.Sum(x => x.Male)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {detailedTransactionPrintModel.Sum(x => x.Cash)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {detailedTransactionPrintModel.Sum(x => x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {detailedTransactionPrintModel.Sum(x => x.Card)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Total Loyalty: {detailedTransactionPrintModel.Count(x => x.Loyalty == 'L')}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"UPI: {detailedTransactionPrintModel.Sum(x => x.UPI)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"Amex: {detailedTransactionPrintModel.Sum(x => x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

		textElement = new PdfTextElement($"Advance: {detailedAdvancePrintModel.Sum(x => x.Adv_Paid)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);

		text = $"Booking: {detailedAdvancePrintModel.Sum(x => x.Booking_Amt)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Redeemed: {detailedAdvancePrintModel.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Adv_Paid)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Redeemed: {detailedAdvancePrintModel.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Booking_Amt)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Not Redeemed: {detailedAdvancePrintModel.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Adv_Paid)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Not Redeemed: {detailedAdvancePrintModel.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Booking_Amt)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);
		#endregion

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}
}
