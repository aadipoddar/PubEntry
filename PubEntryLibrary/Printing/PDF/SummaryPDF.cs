using PubEntryLibrary.Data;

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

using PointF = Syncfusion.Drawing.PointF;

namespace PubEntryLibrary.Printing.PDF;

internal static class SummaryPDF
{
	private static PdfLayoutFormat _layoutFormat;

	internal static async Task<MemoryStream> Print(DateTime fromDateTime, DateTime toDateTime)
	{
		PdfDocument pdfDocument = new();
		PdfPage pdfPage = pdfDocument.Pages.Add();

		pdfDocument.Template.Top = CommonPDF.AddHeader(pdfDocument, $"{fromDateTime} - {toDateTime}", "Summary Report");
		pdfDocument.Template.Bottom = CommonPDF.AddFooter(pdfDocument);

		_layoutFormat = new()
		{
			Layout = PdfLayoutType.Paginate,
			Break = PdfLayoutBreakType.FitPage
		};

		PdfLayoutResult result = null;

		var locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);

		foreach (var location in locations)
		{
			result = await CommonPDF.LocationHeader(location.Id, pdfPage, result);
			result = await TransactionTotals(fromDateTime, toDateTime, location.Id, pdfPage, result);
			result = await AdvanceTotals(fromDateTime, toDateTime, location.Id, pdfPage, result);
		}

		await GrandTotals(fromDateTime, toDateTime, pdfPage, result);

		MemoryStream ms = new();
		pdfDocument.Save(ms);
		pdfDocument.Close(true);
		ms.Position = 0;

		return ms;
	}

	private static async Task<PdfLayoutResult> TransactionTotals(DateTime fromDateTime, DateTime toDateTime, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		List<TransactionTotalsModel> transactionTotalsModel = [];

		transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(fromDateTime, toDateTime, locationId));
		if (transactionTotalsModel.LastOrDefault() is null)
		{
			transactionTotalsModel.Remove(transactionTotalsModel.LastOrDefault());
			transactionTotalsModel.Add(new TransactionTotalsModel());
			transactionTotalsModel.LastOrDefault().LocationId = locationId;
		}

		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

		var textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Last().Male + transactionTotalsModel.Last().Female}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		string text = $"Total Amount: {transactionTotalsModel.Last().Cash + transactionTotalsModel.Last().Card + transactionTotalsModel.Last().UPI + transactionTotalsModel.Last().Amex + transactionTotalsModel.Last().OnlineQR}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Male: {transactionTotalsModel.Last().Male}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Cash: {transactionTotalsModel.Last().Cash}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Female: {transactionTotalsModel.Last().Female}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Card: {transactionTotalsModel.Last().Card}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Loyalty: {transactionTotalsModel.Last().Loyalty}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"UPI: {transactionTotalsModel.Last().UPI}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		text = $"Amex: {transactionTotalsModel.Last().Amex}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Online QR: {transactionTotalsModel.Last().OnlineQR}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), _layoutFormat);

		return result;
	}

	private static async Task<PdfLayoutResult> AdvanceTotals(DateTime fromDateTime, DateTime toDateTime, int locationId, PdfPage pdfPage, PdfLayoutResult result)
	{
		List<AdvanceTotalsModel> advanceTotalsModel = [];

		advanceTotalsModel.Add(toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
						await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), locationId)
						: await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.Date, locationId));
		if (advanceTotalsModel.LastOrDefault() is null)
		{
			advanceTotalsModel.Remove(advanceTotalsModel.LastOrDefault());
			advanceTotalsModel.Add(new AdvanceTotalsModel());
		}

		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

		var textElement = new PdfTextElement($"Total Advance: {advanceTotalsModel.Last().TotalAdvance}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		string text = $"Total Booking: {advanceTotalsModel.Last().TotalBooking}";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Redeemed: {advanceTotalsModel.Last().RedeemedAdvance}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Redeemed: {advanceTotalsModel.Last().RedeemedBooking}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Not Redeemed: {advanceTotalsModel.Last().NotRedeemedAdvance}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Not Redeemed: {advanceTotalsModel.Last().NotRedeemedBooking}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		return result;
	}

	private static async Task GrandTotals(DateTime fromDateTime, DateTime toDateTime, PdfPage pdfPage, PdfLayoutResult result)
	{
		var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20, PdfFontStyle.Bold);

		string text = "Grand Total";
		float textWidth = font.MeasureString(text).Width;
		float pageWidth = pdfPage.GetClientSize().Width;
		float textX = (pageWidth - textWidth) / 2f;

		PdfTextElement textElement = new(text, font);

		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 20), _layoutFormat);

		List<TransactionTotalsModel> transactionTotalsModel = [];
		List<AdvanceTotalsModel> advanceTotalsModel = [];

		var locations = await CommonData.LoadTableDataByStatus<LocationModel>(TableNames.Location);
		foreach (var location in locations)
		{
			transactionTotalsModel.Add(await TransactionData.LoadTransactionTotalsByDateLocation(fromDateTime, toDateTime, location.Id));
			if (transactionTotalsModel.LastOrDefault() is null)
			{
				transactionTotalsModel.Remove(transactionTotalsModel.LastOrDefault());
				transactionTotalsModel.Add(new TransactionTotalsModel());
				transactionTotalsModel.LastOrDefault().LocationId = location.Id;
			}
			advanceTotalsModel.Add(toDateTime.TimeOfDay < TimeSpan.FromHours(17) ?
				await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59), location.Id)
							: await AdvanceData.LoadAdvanceTotalsByForDateLocation(fromDateTime.Date, toDateTime.Date, location.Id));
			if (advanceTotalsModel.LastOrDefault() is null)
			{
				advanceTotalsModel.Remove(advanceTotalsModel.LastOrDefault());
				advanceTotalsModel.Add(new AdvanceTotalsModel());
				advanceTotalsModel.LastOrDefault().LocationId = location.Id;
			}
		}

		font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

		textElement = new PdfTextElement($"Total People: {transactionTotalsModel.Sum(x => x.Male + x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Total Amount: {transactionTotalsModel.Sum(x => x.Cash + x.Card + x.UPI + x.Amex + x.OnlineQR)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Male: {transactionTotalsModel.Sum(x => x.Male)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Cash: {transactionTotalsModel.Sum(x => x.Cash)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Female: {transactionTotalsModel.Sum(x => x.Female)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Card: {transactionTotalsModel.Sum(x => x.Card)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Loyalty: {transactionTotalsModel.Sum(x => x.Loyalty)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"UPI: {transactionTotalsModel.Sum(x => x.UPI)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		text = $"Amex: {transactionTotalsModel.Sum(x => x.Amex)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Online QR: {transactionTotalsModel.Sum(x => x.OnlineQR)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Bottom + 10), _layoutFormat);


		font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

		textElement = new PdfTextElement($"Total Advance: {advanceTotalsModel.Sum(x => x.TotalAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Total Booking: {advanceTotalsModel.Sum(x => x.TotalBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Redeemed: {advanceTotalsModel.Sum(x => x.RedeemedAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Redeemed: {advanceTotalsModel.Sum(x => x.RedeemedBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);

		textElement = new PdfTextElement($"Not Redeemed: {advanceTotalsModel.Sum(x => x.NotRedeemedAdvance)}", font);
		result = textElement.Draw(result.Page, new PointF(10, result.Bounds.Bottom + 10), _layoutFormat);

		text = $"Not Redeemed: {advanceTotalsModel.Sum(x => x.NotRedeemedBooking)}";
		textWidth = font.MeasureString(text).Width;
		pageWidth = pdfPage.GetClientSize().Width;
		textX = pageWidth - textWidth;
		textElement = new PdfTextElement(text, font);
		result = textElement.Draw(result.Page, new PointF(textX, result.Bounds.Top), _layoutFormat);
	}
}
