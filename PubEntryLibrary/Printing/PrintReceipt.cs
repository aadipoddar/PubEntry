using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.Versioning;

using PubEntryLibrary.Data;
using PubEntryLibrary.Models;

namespace PubEntryLibrary.Printing;

public static class PrintReceipt
{
	private static string MakeStringHeaderAmount(TransactionModel transaction)
	{
		string finalString = "";

		if (transaction.Cash > 0)
			finalString += "Cash\t";

		if (transaction.Card > 0)
			finalString += "Card\t";

		if (transaction.UPI > 0)
			finalString += "UPI\t";

		if (transaction.Amex > 0)
			finalString += "Amex\t";

		return finalString;
	}

	private static string MakeStringAmount(TransactionModel transaction)
	{
		string finalString = "";

		if (transaction.Cash > 0)
			finalString += $"{transaction.Cash}\t";

		if (transaction.Card > 0)
			finalString += $"{transaction.Card}\t";

		if (transaction.UPI > 0)
			finalString += $"{transaction.UPI}\t";

		if (transaction.Amex > 0)
			finalString += $"{transaction.Amex}\t";

		return finalString;
	}

	[SupportedOSPlatform("windows6.1")]
	public static void DrawGraphics(PrintPageEventArgs e, string copyOf, int locationId, TransactionModel transaction, int slipId, string numberTextBoxText, bool loyaltyCheckBoxChecked)
	{
		Graphics g = e.Graphics;
		Font font = new("Courier New", 12, FontStyle.Bold);

		StringFormat center = new(StringFormatFlags.FitBlackBox);
		center.Alignment = StringAlignment.Center;

		int lowerSpacing = 0;
		int lowerSpacingIncrement = 15;
		int startPosition = 10;
		int maxWidth = e.PageBounds.Width - 20;

		g.DrawString($"** {Task.Run(async () => await CommonData.GetById<LocationModel>("LocationTable", locationId)).Result.FirstOrDefault().Name} **", new Font("Arial", 25, FontStyle.Bold), Brushes.Black, new RectangleF(startPosition, lowerSpacing += 10, maxWidth, 40), center);
		g.DrawString($"--- {copyOf} Copy ---", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += 40, maxWidth, 25), center);
		g.DrawString($"Slip No.: {slipId}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"DT: {transaction.DateTime.ToString("dd/MM/yy HH:mm")}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"Name: {Task.Run(async () => await PersonData.GetPersonByNumber(numberTextBoxText)).Result.FirstOrDefault().Name}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"Contact: {numberTextBoxText}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		if (loyaltyCheckBoxChecked) g.DrawString("Loyalty Member", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"Reservation: {Task.Run(async () => await CommonData.GetById<ReservationTypeModel>("ReservationTypeTable", transaction.ReservationType)).Result.FirstOrDefault().Name}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));

		g.DrawString("--------------------------", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25), center);
		g.DrawString($"Total Persons: {transaction.Male + transaction.Female}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString("Male\tFemale", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"{transaction.Male}\t{transaction.Female}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));

		g.DrawString("--------------------------", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25), center);
		font = new Font("Arial", 20, FontStyle.Bold);
		g.DrawString($"Total: {transaction.Cash + transaction.Card + transaction.UPI + transaction.Amex}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 35));
		font = new("Courier New", 12, FontStyle.Bold);
		g.DrawString($"{MakeStringHeaderAmount(transaction)}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += 20, maxWidth, 25));
		g.DrawString($"{MakeStringAmount(transaction)}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));

		g.DrawString("--------------------------", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25), center);
		if (transaction.ApprovedBy != null) g.DrawString($"Approved By: {transaction.ApprovedBy}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));
		g.DrawString($"Entered By: {Task.Run(async () => await CommonData.GetById<EmployeeModel>("EmployeeTable", transaction.EmployeeId)).Result.FirstOrDefault().Name}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 25));

		font = new("Courier New", 9, FontStyle.Bold);

		lowerSpacingIncrement = 13;

		startPosition = 8;

		g.DrawString("This coupon is non-transferable", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += 30, maxWidth, 14));
		g.DrawString("to any person or any other outlet.", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("This coupon is to be redeemed", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("until the end of the operations", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("of the particular night:", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString($"{transaction.DateTime.ToString("dd/MM/yy HH:mm")}", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("The hotel does not take liability", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("or responsibility if the coupon", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));
		g.DrawString("is lost by the guest", font, Brushes.Black, new RectangleF(startPosition, lowerSpacing += lowerSpacingIncrement, maxWidth, 14));

		PaperSize ps58 = new PaperSize("58mm Thermal", 220, lowerSpacing += 20);
		e.PageSettings.PaperSize = ps58;

		e.HasMorePages = false;
	}
}