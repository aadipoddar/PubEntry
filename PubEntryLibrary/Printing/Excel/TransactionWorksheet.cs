using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

internal static class TransactionWorksheet
{
	private static int _rowCount;

	internal static async Task AddTransactionWorksheet(IWorkbook workbook, string dateHeader, int locationId, DateTime fromDateTime, DateTime toDateTime)
	{
		IWorksheet worksheet = workbook.Worksheets[0];
		worksheet.Name = "Transaction";

		string locationName = await CommonExecl.GetLocationName(locationId);
		_rowCount = CommonExecl.SetupHeader(worksheet, dateHeader, locationName, "Transaction Details");

		var detailedTransactionPrintModel = await TransactionData.LoadTransactionsByDateLocation(fromDateTime, toDateTime, locationId);
		FillTransactionData(worksheet, detailedTransactionPrintModel);
		FillTransactionTotals(worksheet, detailedTransactionPrintModel);
	}

	private static void FillTransactionData(IWorksheet worksheet, IEnumerable<TransactionPrintModel> detailedTransactionPrintModels)
	{
		string[] headers = ["Slip Id", "Name", "Number", "Loyalty", "Male", "Female", "Cash", "Card", "UPI", "Amex", "Online QR", "Approved By", "Entered By", "Date Time"];
		_rowCount = CommonExecl.FillData(worksheet, headers, _rowCount, detailedTransactionPrintModels, FillTransactionRow);
	}

	private static void FillTransactionRow(IWorksheet worksheet, TransactionPrintModel transaction, int row)
	{
		worksheet.Range[$"A{row}"].Number = transaction.Id;
		worksheet.Range[$"B{row}"].Text = transaction.PersonName;
		worksheet.Range[$"C{row}"].Number = long.Parse(transaction.PersonNumber);
		worksheet.Range[$"D{row}"].Text = transaction.Loyalty.ToString();
		worksheet.Range[$"E{row}"].Number = transaction.Male;
		worksheet.Range[$"F{row}"].Number = transaction.Female;
		worksheet.Range[$"G{row}"].Number = transaction.Cash;
		worksheet.Range[$"H{row}"].Number = transaction.Card;
		worksheet.Range[$"I{row}"].Number = transaction.UPI;
		worksheet.Range[$"J{row}"].Number = transaction.Amex;
		worksheet.Range[$"K{row}"].Number = transaction.OnlineQR;
		worksheet.Range[$"L{row}"].Text = transaction.ApprovedBy;
		worksheet.Range[$"M{row}"].Text = transaction.EnteredBy;
		worksheet.Range[$"N{row}"].Text = transaction.DateTime.ToString("dd/MM/yy HH:mm");
	}

	private static void FillTransactionTotals(IWorksheet worksheet, IEnumerable<TransactionPrintModel> detailedTransactionPrintModels)
	{
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 2}", "Total People :", 20, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 3}", "Total Male :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 4}", "Total Female :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 5}", "Total Loyalty :", 15, ExcelHAlign.HAlignCenter);

		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 2}", detailedTransactionPrintModels.Sum(x => x.Male + x.Female), 20, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 3}", detailedTransactionPrintModels.Sum(x => x.Male), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 4}", detailedTransactionPrintModels.Sum(x => x.Female), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 5}", detailedTransactionPrintModels.Count(x => x?.Loyalty == 'L'), 15, ExcelHAlign.HAlignRight);

		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 2}", "Total Amount :", 20, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 3}", "Total Cash :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 4}", "Total Card :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 5}", "Total UPI :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 6}", "Total Amex :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 7}", "Total Online QR :", 15, ExcelHAlign.HAlignCenter);

		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 2}", detailedTransactionPrintModels.Sum(x => x.Cash + x.Card + x.UPI + x.Amex + x.OnlineQR), 20, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 3}", detailedTransactionPrintModels.Sum(x => x.Cash), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 4}", detailedTransactionPrintModels.Sum(x => x.Card), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 5}", detailedTransactionPrintModels.Sum(x => x.UPI), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 6}", detailedTransactionPrintModels.Sum(x => x.Amex), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 7}", detailedTransactionPrintModels.Sum(x => x.OnlineQR), 15, ExcelHAlign.HAlignRight);
	}
}
