using PubEntryLibrary.Data;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

using PointF = Syncfusion.Drawing.PointF;

namespace PubEntryLibrary.Printing.PDF;

public static class SummaryPrint
{
	public static async Task<MemoryStream> PrintSummary(DateTime fromDateTime, DateTime toDateTime)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = HeaderFooterReport.AddHeader(pdfDocument, dateHeader, "Summary Report");
		pdfDocument.Template.Bottom = HeaderFooterReport.AddFooter(pdfDocument);

		PdfLayoutFormat layoutFormat = new()
		{
			Layout = PdfLayoutType.Paginate,
			Break = PdfLayoutBreakType.FitPage
		};

		PdfStandardFont font = new(PdfFontFamily.Helvetica, 25, PdfFontStyle.Bold);

		PdfLayoutResult result = null;
		PdfTextElement textElement;
		List<TransactionTotalsModel> transactionTotalsModel = [];
		List<AdvanceTotalsModel> advanceTotalsModel = [];

		string text;
		float textWidth, pageWidth, textX;
		var locations = await CommonData.LoadTableDataByStatus<LocationModel>(Table.Location);

		foreach (var location in locations)
		{
			font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

			text = $"{location.Name}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = (pageWidth - textWidth) / 2f;
			textElement = new PdfTextElement(text, font);
			if (result == null) result = textElement.Draw(pdfPage, new PointF(textX, 20), layoutFormat);
			else result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

			#region Transactions

			transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(fromDateTime, toDateTime, location.Id));
			if (transactionTotalsModel.LastOrDefault() is null)
			{
				transactionTotalsModel.Remove(transactionTotalsModel.LastOrDefault());
				transactionTotalsModel.Add(new TransactionTotalsModel());
				transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

			textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Last().Male + transactionTotalsModel.Last().Female}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Total Amount: {transactionTotalsModel.Last().Cash + transactionTotalsModel.Last().Card + transactionTotalsModel.Last().UPI + transactionTotalsModel.Last().Amex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Male: {transactionTotalsModel.Last().Male}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Cash: {transactionTotalsModel.Last().Cash}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Female: {transactionTotalsModel.Last().Female}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Card: {transactionTotalsModel.Last().Card}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Loyalty: {transactionTotalsModel.Last().Loyalty}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"UPI: {transactionTotalsModel.Last().UPI}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			text = $"Amex: {transactionTotalsModel.Last().Amex}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);

			#endregion

			#region Advance

			advanceTotalsModel.Add(toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
				: await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.Date, location.Id));
			if (advanceTotalsModel.LastOrDefault() is null)
			{
				advanceTotalsModel.Remove(advanceTotalsModel.LastOrDefault());
				advanceTotalsModel.Add(new AdvanceTotalsModel());
			}

			font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

			textElement = new PdfTextElement($"Total Advance: {advanceTotalsModel.Last().TotalAdvance}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Total Booking: {advanceTotalsModel.Last().TotalBooking}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Redeemed: {advanceTotalsModel.Last().RedeemedAdvance}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Redeemed: {advanceTotalsModel.Last().RedeemedBooking}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			textElement = new PdfTextElement($"Not Redeemed: {advanceTotalsModel.Last().NotRedeemedAdvance}", font);
			result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

			text = $"Not Redeemed: {advanceTotalsModel.Last().NotRedeemedBooking}";
			textWidth = font.MeasureString(text).Width;
			pageWidth = pdfPage.GetClientSize().Width;
			textX = pageWidth - textWidth;
			textElement = new PdfTextElement(text, font);
			result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

			#endregion
		}

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);
		text = "Grand Total";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = (pageWidth - textWidth) / 2f;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), layoutFormat);

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

		textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Sum(x => x.Male + x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Amount: {transactionTotalsModel.Sum(x => x.Cash + x.Card + x.UPI + x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Male: {transactionTotalsModel.Sum(x => x.Male)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Cash: {transactionTotalsModel.Sum(x => x.Cash)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Female: {transactionTotalsModel.Sum(x => x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Card: {transactionTotalsModel.Sum(x => x.Card)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Loyalty: {transactionTotalsModel.Sum(x => x.Loyalty)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"UPI: {transactionTotalsModel.Sum(x => x.UPI)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		text = $"Amex: {transactionTotalsModel.Sum(x => x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), layoutFormat);


		font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

		textElement = new PdfTextElement($"Total Advance: {advanceTotalsModel.Sum(x => x.TotalAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Total Booking: {advanceTotalsModel.Sum(x => x.TotalBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Redeemed: {advanceTotalsModel.Sum(x => x.RedeemedAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Redeemed: {advanceTotalsModel.Sum(x => x.RedeemedBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		textElement = new PdfTextElement($"Not Redeemed: {advanceTotalsModel.Sum(x => x.NotRedeemedAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), layoutFormat);

		text = $"Not Redeemed: {advanceTotalsModel.Sum(x => x.NotRedeemedBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), layoutFormat);

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}
}
