using System.Windows;
using System.Windows.Documents;

using NumericWordsConversion;

namespace PubEntryWPF.Transaction.Printing;

internal static class ThermalReceipt
{
	private static int PageWidthThermal => (int)Application.Current.Resources[SettingsKeys.PageWidthThermal];
	private static int PagePaddingTopThermal => (int)Application.Current.Resources[SettingsKeys.PagePaddingTopThermal];
	private static int PagePaddingBottomThermal => (int)Application.Current.Resources[SettingsKeys.PagePaddingBottomThermal];
	private static int PagePaddingLeftThermal => (int)Application.Current.Resources[SettingsKeys.PagePaddingLeftThermal];
	private static int PagePaddingRightThermal => (int)Application.Current.Resources[SettingsKeys.PagePaddingRightThermal];
	private static string FooterLine1 => Application.Current.Resources[SettingsKeys.FooterLine1].ToString();
	private static string FooterLine2 => Application.Current.Resources[SettingsKeys.FooterLine2].ToString();

	internal static FlowDocument Print(TransactionPrintModel receiptModel, string copyOf, int advance)
	{
		FlowDocument document = new()
		{
			PageWidth = PageWidthThermal,
			PagePadding = new Thickness(PagePaddingLeftThermal, PagePaddingTopThermal, PagePaddingRightThermal, PagePaddingBottomThermal),
			ColumnWidth = double.MaxValue
		};

		AddHeader(document, receiptModel, copyOf);
		AddReceiptDetails(document, receiptModel);
		AddPersonDetails(document, receiptModel);
		AddPaymentDetails(document, receiptModel, advance);
		AddFooterDetails(document, receiptModel);

		return document;
	}

	private static void AddHeader(FlowDocument document, TransactionPrintModel receiptModel, string copyOf)
	{
		document.Blocks.Add(ThermalParagraphs.HeaderParagraph($"** {receiptModel.LocationName} **"));
		document.Blocks.Add(ThermalParagraphs.SubHeaderParagraph($"--- {copyOf} Copy ---"));
	}

	private static void AddReceiptDetails(FlowDocument document, TransactionPrintModel receiptModel)
	{
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Receipt No.: {receiptModel.Id}"));
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"DT: {receiptModel.DateTime:dd/MM/yy HH:mm}"));
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Name: {receiptModel.PersonName}"));
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Contact: {receiptModel.PersonNumber}"));

		if (receiptModel.Loyalty == 'L') document.Blocks.Add(ThermalParagraphs.RegularParagraph("Loyalty Member"));

		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Reservation: {receiptModel.Reservation}"));
		document.Blocks.Add(ThermalParagraphs.SeparatorParagraph());
	}

	private static void AddPersonDetails(FlowDocument document, TransactionPrintModel receiptModel)
	{
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Total Persons: {receiptModel.Male + receiptModel.Female}"));

		Table genderTable = new()
		{
			Margin = new Thickness(0, 5, 0, 5)
		};

		genderTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
		genderTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });

		TableRowGroup group = new();
		group.Rows.Add(new TableRow
		{
			Cells =
			{
				new TableCell(ThermalParagraphs.RegularParagraph("Male")),
				new TableCell(ThermalParagraphs.RegularParagraph("Female"))
			}
		});

		group.Rows.Add(new TableRow
		{
			Cells =
			{
				new TableCell(ThermalParagraphs.RegularParagraph(receiptModel.Male.ToString())),
				new TableCell(ThermalParagraphs.RegularParagraph(receiptModel.Female.ToString()))
			}
		});

		genderTable.RowGroups.Add(group);
		document.Blocks.Add(genderTable);
		document.Blocks.Add(ThermalParagraphs.SeparatorParagraph());
	}

	private static void AddPaymentDetails(FlowDocument document, TransactionPrintModel receiptModel, int advance)
	{
		document.Blocks.Add(ThermalParagraphs.SubHeaderParagraph($"Total: {receiptModel.Cash + receiptModel.Card + receiptModel.UPI + receiptModel.Amex}"));

		if (receiptModel.Cash + receiptModel.Card + receiptModel.UPI + receiptModel.Amex != 0)
		{
			Table paymentTable = new()
			{
				Margin = new Thickness(0, 5, 0, 5)
			};

			paymentTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });
			paymentTable.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) });

			TableRowGroup paymentGroup = new();
			if (advance > 0) ThermalParagraphs.AddTableRow(paymentGroup, "Advance:", advance);
			if (receiptModel.Cash > 0) ThermalParagraphs.AddTableRow(paymentGroup, "Cash:", receiptModel.Cash);
			if (receiptModel.Card > 0) ThermalParagraphs.AddTableRow(paymentGroup, "Card:", receiptModel.Card);
			if (receiptModel.UPI > 0) ThermalParagraphs.AddTableRow(paymentGroup, "UPI:", receiptModel.UPI);
			if (receiptModel.Amex > 0) ThermalParagraphs.AddTableRow(paymentGroup, "Amex:", receiptModel.Amex);

			paymentTable.RowGroups.Add(paymentGroup);
			document.Blocks.Add(paymentTable);
		}

		CurrencyWordsConverter numericWords = new(new()
		{
			Culture = Culture.Hindi,
			OutputFormat = OutputFormat.English
		});
		string words = numericWords.ToWords(receiptModel.Cash + receiptModel.Card + receiptModel.UPI + receiptModel.Amex);
		document.Blocks.Add(ThermalParagraphs.FooterParagraph($"{words}Rupees Only", true));

		document.Blocks.Add(ThermalParagraphs.SeparatorParagraph());
	}

	private static void AddFooterDetails(FlowDocument document, TransactionPrintModel receiptModel)
	{
		if (receiptModel.ApprovedBy != string.Empty) document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Approved By: {receiptModel.ApprovedBy}"));
		document.Blocks.Add(ThermalParagraphs.RegularParagraph($"Entered By: {receiptModel.EnteredBy}"));

		document.Blocks.Add(ThermalParagraphs.FooterParagraph(FooterLine1));
		document.Blocks.Add(ThermalParagraphs.FooterParagraph($"{receiptModel.DateTime:dd/MM/yy HH:mm}"));
		document.Blocks.Add(ThermalParagraphs.FooterParagraph(FooterLine2));
	}
}