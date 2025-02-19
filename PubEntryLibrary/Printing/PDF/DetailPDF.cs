using PubEntryLibrary.Data;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using PointF = Syncfusion.Drawing.PointF;

namespace PubEntryLibrary.Printing.PDF;

internal static class DetailPDF
{
	private static PdfLayoutFormat _layoutFormat;

	internal static async Task<MemoryStream> Print(DateTime fromDateTime, DateTime toDateTime, int locationId)
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = CommonPDF.AddHeader(pdfDocument, $"{fromDateTime} - {toDateTime}", "Detail Report");
		pdfDocument.Template.Bottom = CommonPDF.AddFooter(pdfDocument);

		_layoutFormat = new()
		{
			Layout = PdfLayoutType.Paginate,
			Break = PdfLayoutBreakType.FitPage
		};

		PdfLayoutResult result = null;

		result = await CommonPDF.LocationHeader(locationId, pdfPage, result);
		result = await TransactionDetails(fromDateTime, toDateTime, locationId, pdfPage, result);
		result = await AdvanceDetails(fromDateTime, toDateTime, locationId, pdfPage, result);
		await Totals(fromDateTime, toDateTime, locationId, pdfPage, result);

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}

	private static async Task<PdfLayoutResult> TransactionDetails(DateTime fromDateTime, DateTime toDateTime, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		string text = "Transaction Details";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		PdfTextElement textElement = new(text, font);
		PdfGrid pdfGrid;

		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), _layoutFormat);

		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(fromDateTime, toDateTime, locationId);
		if (detailedTransactionPrintModel.Count > 0)
		{
			pdfGrid = new PdfGrid()
			{
				DataSource = detailedTransactionPrintModel
				.Select(x => new
				{
					x.Id,
					Name = x.PersonName,
					Number = x.PersonNumber,
					x.Loyalty,
					x.Male,
					x.Female,
					x.Cash,
					x.Card,
					x.UPI,
					x.Amex,
					Remarks = x.ApprovedBy,
					x.EnteredBy,
					x.DateTime
				}).ToList()
			};

			pdfGrid.Columns[0].Width = 30;
			pdfGrid.Columns[2].Width = 50;
			pdfGrid.Columns[3].Width = 27;
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

			result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), _layoutFormat);
		}

		return result;
	}

	private static async Task<PdfLayoutResult> AdvanceDetails(DateTime fromDateTime, DateTime toDateTime, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		string text = "Advance Details";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		PdfTextElement textElement = new(text, font);
		PdfGrid pdfGrid;

		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), _layoutFormat);

		var detailedAdvancePrintModel = toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
			 await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), locationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.Date, locationId);
		if (detailedAdvancePrintModel.Count > 0)
		{
			pdfGrid = new() { DataSource = detailedAdvancePrintModel };

			pdfGrid.Columns[0].Width = 30;
			pdfGrid.Columns[2].Width = 50;
			pdfGrid.Columns[3].Width = 27;
			pdfGrid.Columns[4].Width = 40;
			pdfGrid.Columns[5].Width = 40;
			pdfGrid.Columns[6].Width = 40;
			pdfGrid.Columns[11].Width = 30;

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

			result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), _layoutFormat);
		}

		return result;
	}

	private static async Task Totals(DateTime fromDateTime, DateTime toDateTime, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(fromDateTime, toDateTime, locationId);

		var detailedAdvancePrintModel = toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
			 await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), locationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.Date, locationId);

		PdfTextElement textElement = new($"Total People: {detailedTransactionPrintModel.Sum(x => x.Male + x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		string text = $"Total Amount: {detailedTransactionPrintModel.Sum(x => x.Cash + x.Card + x.UPI + x.Amex)}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Male: {detailedTransactionPrintModel.Sum(x => x.Male)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Cash: {detailedTransactionPrintModel.Sum(x => x.Cash)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Female: {detailedTransactionPrintModel.Sum(x => x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Card: {detailedTransactionPrintModel.Sum(x => x.Card)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Loyalty: {detailedTransactionPrintModel.Count(x => x.Loyalty == 'L')}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"UPI: {detailedTransactionPrintModel.Sum(x => x.UPI)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		text = $"Amex: {detailedTransactionPrintModel.Sum(x => x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), _layoutFormat);

		textElement = new PdfTextElement($"Advance: {detailedAdvancePrintModel.Sum(x => x.Amount)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), _layoutFormat);

		text = $"Booking: {detailedAdvancePrintModel.Sum(x => x.Booking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Redeemed: {detailedAdvancePrintModel.Where(x => x.SlipId != "NOT REDEEMED").Sum(x => x.Amount)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Redeemed: {detailedAdvancePrintModel.Where(x => x.SlipId != "NOT REDEEMED").Sum(x => x.Booking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Not Redeemed: {detailedAdvancePrintModel.Where(x => x.SlipId == "NOT REDEEMED").Sum(x => x.Amount)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Not Redeemed: {detailedAdvancePrintModel.Where(x => x.SlipId == "NOT REDEEMED").Sum(x => x.Booking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);
	}
}
