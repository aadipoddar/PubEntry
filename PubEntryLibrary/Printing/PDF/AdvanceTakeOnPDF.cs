using PubEntryLibrary.Data;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

using PointF = Syncfusion.Drawing.PointF;

namespace PubEntryLibrary.Printing.PDF;

internal static class AdvanceTakeOnPDF
{
	private static PdfLayoutFormat _layoutFormat;

	internal static async Task<MemoryStream> Print(DateTime takenOn, int locationId)
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = CommonPDF.AddHeader(pdfDocument, takenOn.ToLongDateString(), "Detail Report");
		pdfDocument.Template.Bottom = CommonPDF.AddFooter(pdfDocument);

		_layoutFormat = new()
		{
			Layout = PdfLayoutType.Paginate,
			Break = PdfLayoutBreakType.FitPage
		};

		PdfLayoutResult result = null;

		result = await CommonPDF.LocationHeader(locationId, pdfPage, result);
		result = await AdvanceDetails(takenOn, locationId, pdfPage, result);
		await AdvanceTotals(takenOn, locationId, pdfPage, result);

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}

	private static async Task<PdfLayoutResult> AdvanceDetails(DateTime takenOn, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		string text = "Advance Details";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		PdfTextElement textElement = new(text, font);
		PdfGrid pdfGrid;

		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), _layoutFormat);

		var detailedAdvancePrintModel = await AdvanceData.LoadAdvancesByTakenOnLocation(takenOn, locationId);

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

	private static async Task<PdfLayoutResult> AdvanceTotals(DateTime takenOn, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		string text = "Advance Totals";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		PdfTextElement textElement = new(text, font);
		PdfGrid pdfGrid;

		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), _layoutFormat);

		var advancePaymentModeTotals = await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(takenOn, locationId);

		if (advancePaymentModeTotals.Count > 0)
		{
			pdfGrid = new() { DataSource = advancePaymentModeTotals };

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
}
