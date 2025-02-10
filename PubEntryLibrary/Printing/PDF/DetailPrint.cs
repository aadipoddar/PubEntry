using PubEntryLibrary.Data;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using PointF = Syncfusion.Drawing.PointF;

namespace PubEntryLibrary.Printing.PDF;

public static class DetailPrint
{
	public static async Task<MemoryStream> PrintDetail(DateTime fromDateTime, DateTime toDateTime, int selectedLocationId)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = HeaderFooterReport.AddHeader(pdfDocument, dateHeader, "Detail Report");
		pdfDocument.Template.Bottom = HeaderFooterReport.AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new()
		{
			Layout = PdfLayoutType.Paginate,
			Break = PdfLayoutBreakType.FitPage
		};

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		string text = $"{(await CommonData.LoadTableDataById<LocationModel>(Table.Location, selectedLocationId)).Name}";
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

		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(fromDateTime, toDateTime, selectedLocationId);

		if (detailedTransactionPrintModel.Count() > 0)
		{
			pdfGrid.DataSource = detailedTransactionPrintModel
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
				}).ToList();

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

			result = pdfGrid.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 20), layoutFormat);
		}

		#endregion

		#region AdvanceDetails

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
		text = "Advance Details";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		var detailedAdvancePrintModel = toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
			 await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), selectedLocationId)
			: await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime.Date, toDateTime.Date, selectedLocationId);

		if (detailedAdvancePrintModel.Count() > 0)
		{
			pdfGrid = new() { DataSource = detailedAdvancePrintModel };

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

		textElement = new PdfTextElement($"Total People: {detailedTransactionPrintModel.Sum(x => x.Male + x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {detailedTransactionPrintModel.Sum(x => x.Cash + x.Card + x.UPI + x.Amex)}";
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

		textElement = new PdfTextElement($"Loyalty: {detailedTransactionPrintModel.Count(x => x.Loyalty == 'L')}", font);
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
