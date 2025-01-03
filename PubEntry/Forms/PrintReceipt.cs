﻿using System.Drawing.Printing;

using PubEntryLibrary.Models.Printing;

namespace PubEntry;

public static class PrintReceipt
{
	private static readonly Font headerFont = new("Arial", 25, FontStyle.Bold);
	private static readonly Font subHeaderFont = new("Arial", 15, FontStyle.Bold);
	private static readonly Font regularFont = new("Courier New", 12, FontStyle.Bold);
	private static readonly Font footerFont = new("Courier New", 8, FontStyle.Bold);
	private static Font font = regularFont;

	private static readonly StringFormat center = new(StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center };
	private static readonly StringFormat tabbedFormat = new();
	private static int startPosition = 10;
	private static int lowerSpacing = 0;
	private static int maxWidth;

	static PrintReceipt() => tabbedFormat.SetTabStops(0, [100]);

	private static void DrawString(Graphics g, string content, bool isCenter = false, bool useTabs = false)
	{
		StringFormat format = isCenter ? center : (useTabs ? tabbedFormat : new StringFormat());
		SizeF size = g.MeasureString(content, font, maxWidth, format);
		g.DrawString(content, font, Brushes.Black, new RectangleF(startPosition, lowerSpacing, maxWidth, size.Height), format);
		lowerSpacing += (int)size.Height;
	}

	private static void DrawHeader(Graphics g, string locationName, string copyOf)
	{
		font = headerFont;
		DrawString(g, $"** {locationName} **", true);

		font = subHeaderFont;
		DrawString(g, $"--- {copyOf} Copy ---", true);
	}

	private static void DrawReceiptDetails(Graphics g, ReceiptModel receiptModel)
	{
		font = regularFont;
		DrawString(g, $"Receipt No.: {receiptModel.ReceiptId}");
		DrawString(g, $"DT: {receiptModel.ReceiptDate:dd/MM/yy HH:mm}");
		DrawString(g, $"Name: {receiptModel.PersonName}");
		DrawString(g, $"Contact: {receiptModel.PersonNumber}");
		if (receiptModel.PersonLoyalty) DrawString(g, "Loyalty Member");
		DrawString(g, $"Reservation: {receiptModel.Reservation}");
		DrawString(g, "------------------------", true);

		DrawString(g, $"Total Persons: {receiptModel.TotalPerson}");
		DrawString(g, "Male\tFemale", false, true);
		DrawString(g, $"{receiptModel.Male}\t{receiptModel.Female}", false, true);
		DrawString(g, "------------------------", true);
	}

	private static void DrawPaymentDetails(Graphics g, ReceiptModel receiptModel, List<ReceiptPaymentModel> transactionReceiptPayments, List<ReceiptPaymentModel> advanceReceiptPayments)
	{
		font = subHeaderFont;
		int totalPaymentAmount = transactionReceiptPayments.Sum(payment => payment.Amount) + advanceReceiptPayments.Sum(payment => payment.Amount);
		DrawString(g, $"Total: {totalPaymentAmount}");

		font = regularFont;
		if (advanceReceiptPayments.Count > 0) DrawString(g, $"Advance: {advanceReceiptPayments.Sum(payment => payment.Amount)}");
		transactionReceiptPayments.ForEach(payment => DrawString(g, $"{payment.PaymentMode}: {payment.Amount}"));

		DrawString(g, "------------------------", true);
	}

	private static void DrawFooter(Graphics g, ReceiptModel receiptModel)
	{
		if (receiptModel.ApprovedBy != string.Empty) DrawString(g, $"Approved By: {receiptModel.ApprovedBy}");
		DrawString(g, $"Entered By: {receiptModel.EnteredBy}");

		font = footerFont;
		DrawString(g, "This coupon is non-transferable");
		DrawString(g, "to any person or any other outlet.");
		DrawString(g, "This coupon is to be redeemed");
		DrawString(g, "until the end of the operations");
		DrawString(g, "of the particular night:");
		DrawString(g, $"{receiptModel.ReceiptDate:dd/MM/yy HH:mm}");
		DrawString(g, "The hotel does not take liability");
		DrawString(g, "or responsibility if the coupon");
		DrawString(g, "is lost by the guest");
	}

	public static void DrawGraphics(PrintPageEventArgs e, string copyOf, int transactionId, int advanceId)
	{
		ReceiptModel receiptModel = Task.Run(async () => await PrintData.LoadReceiptDetails(transactionId)).Result.FirstOrDefault();
		List<ReceiptPaymentModel> transactionReceiptPayments = Task.Run(async () => await PaymentData.LoadPaymentsById(transactionId)).Result;
		List<ReceiptPaymentModel> advanceReceiptPayments = Task.Run(async () => await PaymentData.LoadPaymentsById(0, advanceId)).Result;

		Graphics g = e.Graphics;
		maxWidth = e.PageBounds.Width - 20;
		startPosition = 10;
		lowerSpacing = 0;

		DrawHeader(g, receiptModel.LocationName, copyOf);
		DrawReceiptDetails(g, receiptModel);
		DrawPaymentDetails(g, receiptModel, transactionReceiptPayments, advanceReceiptPayments);
		DrawFooter(g, receiptModel);

		PaperSize ps58 = new PaperSize("58mm Thermal", 220, lowerSpacing + 20);
		e.PageSettings.PaperSize = ps58;

		e.HasMorePages = false;
	}
}