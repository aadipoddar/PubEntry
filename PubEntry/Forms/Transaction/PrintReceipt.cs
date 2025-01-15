using System.Drawing.Printing;

namespace PubEntry.Forms.Transaction;

public static class PrintReceipt
{
	private static readonly Font _headerFont;
	private static readonly Font _subHeaderFont;
	private static readonly Font _regularFont;
	private static readonly Font _footerFont;
	private static Font font = _regularFont;

	private static readonly StringFormat center = new(StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center };
	private static readonly StringFormat tabbedFormat = new();
	private static int startPosition = 10;
	private static int lowerSpacing;
	private static int maxWidth;

	static PrintReceipt()
	{
		var settings = Task.Run(async () => await CommonData.LoadTableDataById<SettingsModel>(Table.Settings, 1)).Result;

		_headerFont = new(settings.HeaderFontFamilyThermal, settings.HeaderFontSizeThermal, (FontStyle)settings.HeaderFontStyleThermal);
		_subHeaderFont = new(settings.SubHeaderFontFamilyThermal, settings.SubHeaderFontSizeThermal, (FontStyle)settings.SubHeaderFontStyleThermal);
		_regularFont = new(settings.RegularFontFamilyThermal, settings.RegularFontSizeThermal, (FontStyle)settings.RegularFontStyleThermal);
		_footerFont = new(settings.FooterFontFamilyThermal, settings.FooterFontSizeThermal, (FontStyle)settings.FooterFontStyleThermal);

		tabbedFormat.SetTabStops(0, [100]);
	}

	private static void DrawString(Graphics g, string content, bool isCenter = false, bool useTabs = false)
	{
		StringFormat format = isCenter ? center : useTabs ? tabbedFormat : new StringFormat();
		SizeF size = g.MeasureString(content, font, maxWidth, format);
		g.DrawString(content, font, Brushes.Black, new RectangleF(startPosition, lowerSpacing, maxWidth, size.Height), format);
		lowerSpacing += (int)size.Height;
	}

	private static void DrawHeader(Graphics g, string locationName, string copyOf)
	{
		font = _headerFont;
		DrawString(g, $"** {locationName} **", true);

		font = _subHeaderFont;
		DrawString(g, $"--- {copyOf} Copy ---", true);
	}

	private static void DrawReceiptDetails(Graphics g, TransactionPrintModel receiptModel)
	{
		font = _regularFont;
		DrawString(g, $"Receipt No.: {receiptModel.Id}");
		DrawString(g, $"DT: {receiptModel.DateTime:dd/MM/yy HH:mm}");
		DrawString(g, $"Name: {receiptModel.PersonName}");
		DrawString(g, $"Contact: {receiptModel.PersonNumber}");
		if (receiptModel.Loyalty == 'L') DrawString(g, "Loyalty Member");
		DrawString(g, $"Reservation: {receiptModel.Reservation}");
		DrawString(g, "------------------------", true);

		DrawString(g, $"Total Persons: {receiptModel.Male + receiptModel.Female}");
		DrawString(g, "Male\tFemale", false, true);
		DrawString(g, $"{receiptModel.Male}\t{receiptModel.Female}", false, true);
		DrawString(g, "------------------------", true);
	}

	private static void DrawPaymentDetails(Graphics g, TransactionPrintModel receiptModel, int advance)
	{
		font = _subHeaderFont;
		DrawString(g, $"Total: {receiptModel.Cash + receiptModel.Card + receiptModel.UPI + receiptModel.Amex + advance}");


		font = _regularFont;
		if (advance > 0) DrawString(g, $"Advance: {advance}");
		if (receiptModel.Cash > 0) DrawString(g, $"Cash: {receiptModel.Cash}");
		if (receiptModel.Card > 0) DrawString(g, $"Card: {receiptModel.Card}");
		if (receiptModel.UPI > 0) DrawString(g, $"UPI: {receiptModel.UPI}");
		if (receiptModel.Amex > 0) DrawString(g, $"Amex: {receiptModel.Amex}");
		DrawString(g, "------------------------", true);
	}

	private static void DrawFooter(Graphics g, TransactionPrintModel receiptModel)
	{
		if (receiptModel.ApprovedBy != string.Empty) DrawString(g, $"Approved By: {receiptModel.ApprovedBy}");
		DrawString(g, $"Entered By: {receiptModel.EnteredBy}");

		font = _footerFont;
		DrawString(g, "This coupon is non-transferable");
		DrawString(g, "to any person or any other outlet.");
		DrawString(g, "This coupon is to be redeemed");
		DrawString(g, "until the end of the operations");
		DrawString(g, "of the particular night:");
		DrawString(g, $"{receiptModel.DateTime:dd/MM/yy HH:mm}");
		DrawString(g, "The hotel does not take liability");
		DrawString(g, "or responsibility if the coupon");
		DrawString(g, "is lost by the guest");
	}

	public static void DrawGraphics(PrintPageEventArgs e, string copyOf, int transactionId, int advanceId)
	{
		TransactionPrintModel receiptModel = Task.Run(async () => await CommonData.LoadTableDataById<TransactionPrintModel>(Views.Transactions, transactionId)).Result;

		var advance = 0;
		if (advanceId is not 0) advance = Task.Run(async () => await AdvanceData.LoadAdvanceDetailByAdvanceId(advanceId)).Result.Sum(x => x.Amount);

		Graphics g = e.Graphics;
		maxWidth = e.PageBounds.Width - 20;
		lowerSpacing = 0;

		DrawHeader(g, receiptModel.LocationName, copyOf);
		DrawReceiptDetails(g, receiptModel);
		DrawPaymentDetails(g, receiptModel, advance);
		DrawFooter(g, receiptModel);

		PaperSize ps58 = new("58mm Thermal", 220, lowerSpacing + 20);
		e.PageSettings.PaperSize = ps58;

		e.HasMorePages = false;
	}
}