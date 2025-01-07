using PubEntryLibrary.Data;
using PubEntryLibrary.Models.Printing;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

public static class TransactionWorksheet
{
	private static int _rowCount;

	public static async Task AddTransactionWorksheet(IWorkbook workbook, string dateHeader, int selectedLocationId, DateTime fromDateTime, DateTime toDateTime)
	{
		IWorksheet worksheet = workbook.Worksheets[0];
		worksheet.Name = "Transaction";

		string locationName = await Excel.GetLocationName(selectedLocationId);
		Excel.SetupHeader(worksheet, dateHeader, locationName, "Transaction Details");

		var detailedTransactionPrintModel = await PrintData.LoadTransactionsByDateAndLocation(fromDateTime, toDateTime, selectedLocationId);
		FillTransactionData(worksheet, detailedTransactionPrintModel);
		FillTransactionTotals(worksheet, detailedTransactionPrintModel);
	}

	private static void FillTransactionData(IWorksheet worksheet, IEnumerable<DetailedTransactionPrintModel> detailedTransactionPrintModels)
	{
		string[] headers = { "Slip Id", "Name", "Number", "Loyalty", "Male", "Female", "Cash", "Card", "UPI", "Amex", "Approved By", "Entered By", "Date Time" };
		_rowCount = Excel.FillData(worksheet, headers, detailedTransactionPrintModels, FillTransactionRow);
	}

	private static void FillTransactionRow(IWorksheet worksheet, DetailedTransactionPrintModel transaction, int row)
	{
		worksheet.Range[$"A{row}"].Number = transaction.SlipId;
		worksheet.Range[$"B{row}"].Text = transaction.PersonName;
		worksheet.Range[$"C{row}"].Number = long.Parse(transaction.PersonNumber);
		worksheet.Range[$"D{row}"].Text = transaction.Loyalty.ToString();
		worksheet.Range[$"E{row}"].Number = transaction.Male;
		worksheet.Range[$"F{row}"].Number = transaction.Female;
		worksheet.Range[$"G{row}"].Number = transaction.Cash;
		worksheet.Range[$"H{row}"].Number = transaction.Card;
		worksheet.Range[$"I{row}"].Number = transaction.UPI;
		worksheet.Range[$"J{row}"].Number = transaction.Amex;
		worksheet.Range[$"K{row}"].Text = transaction.ApprovedBy;
		worksheet.Range[$"L{row}"].Text = transaction.EnteredBy;
		worksheet.Range[$"M{row}"].Text = transaction.ReceiptDate.ToString("dd/MM/yy HH:mm");
	}

	private static void FillTransactionTotals(IWorksheet worksheet, IEnumerable<DetailedTransactionPrintModel> detailedTransactionPrintModels)
	{
		int totalRow = _rowCount + 5;

		Excel.SetTotalCell(worksheet, $"B{totalRow + 2}", "Total People :", 20, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"B{totalRow + 3}", "Total Male :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"B{totalRow + 4}", "Total Female :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"B{totalRow + 5}", "Total Loyalty :", 15, ExcelHAlign.HAlignCenter);

		Excel.SetTotalCell(worksheet, $"D{totalRow + 2}", detailedTransactionPrintModels.Sum(x => x.Male + x.Female), 20, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"D{totalRow + 3}", detailedTransactionPrintModels.Sum(x => x.Male), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"D{totalRow + 4}", detailedTransactionPrintModels.Sum(x => x.Female), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"D{totalRow + 5}", detailedTransactionPrintModels.Count(x => x.Loyalty == 'L'), 15, ExcelHAlign.HAlignRight);

		Excel.SetTotalCell(worksheet, $"I{totalRow + 2}", "Total Amount :", 20, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 3}", "Total Cash :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 4}", "Total Card :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 5}", "Total UPI :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 6}", "Total Amex :", 15, ExcelHAlign.HAlignCenter);

		Excel.SetTotalCell(worksheet, $"K{totalRow + 2}", detailedTransactionPrintModels.Sum(x => x.Cash + x.Card + x.UPI + x.Amex), 20, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 3}", detailedTransactionPrintModels.Sum(x => x.Cash), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 4}", detailedTransactionPrintModels.Sum(x => x.Card), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 5}", detailedTransactionPrintModels.Sum(x => x.UPI), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 6}", detailedTransactionPrintModels.Sum(x => x.Amex), 15, ExcelHAlign.HAlignRight);
	}
}
