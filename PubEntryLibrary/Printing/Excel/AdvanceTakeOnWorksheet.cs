using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

internal class AdvanceTakeOnWorksheet
{
	private static int _rowCount;

	internal static async Task AddAdvanceTakeOnWorksheet(IWorkbook workbook, string dateHeader, int locationId, DateTime takenOn)
	{
		IWorksheet worksheet = workbook.Worksheets[0];
		worksheet.Name = "Advance";
		worksheet.Clear();

		string locationName = await CommonExecl.GetLocationName(locationId);
		_rowCount = CommonExecl.SetupHeader(worksheet, dateHeader, locationName, "Advance Details");

		var detailedAdvancePrintModel = await AdvanceData.LoadAdvancesByTakenOnLocation(takenOn, locationId);
		FillAdvanceData(worksheet, detailedAdvancePrintModel);

		_rowCount += 5;

		var advancePaymentModeTotals = await AdvanceData.LoadAdvancePaymentModeTotalsByTakenOn(takenOn, locationId);
		FillAdvanceTotals(worksheet, advancePaymentModeTotals);
	}

	private static void FillAdvanceData(IWorksheet worksheet, IEnumerable<AdvancePrintModel> detailedAdvancePrintModels)
	{
		string[] headers = ["Id", "Name", "Number", "Loyalty", "PaymentDT", "ForDT", "Remarks", "User", "Booking Amt", "Amount", "Mode", "Slip Id", "Entry", "Slip DT", "Total"];
		_rowCount = CommonExecl.FillData(worksheet, headers, _rowCount, detailedAdvancePrintModels, FillAdvanceRow);
	}

	private static void FillAdvanceRow(IWorksheet worksheet, AdvancePrintModel advance, int row)
	{
		worksheet.Range[$"A{row}"].Number = advance.Id;
		worksheet.Range[$"B{row}"].Text = advance.Name;
		worksheet.Range[$"C{row}"].Number = long.Parse(advance.Number);
		worksheet.Range[$"D{row}"].Text = advance.Loyalty.ToString();
		worksheet.Range[$"E{row}"].DateTime = advance.PaymentDT;
		worksheet.Range[$"F{row}"].DateTime = advance.ForDT;
		worksheet.Range[$"G{row}"].Text = advance.Remarks;
		worksheet.Range[$"H{row}"].Text = advance.User;
		worksheet.Range[$"I{row}"].Number = advance.Booking;
		worksheet.Range[$"J{row}"].Number = advance.Amount;
		worksheet.Range[$"K{row}"].Text = advance.Mode;
		worksheet.Range[$"L{row}"].Text = advance.SlipId;
		worksheet.Range[$"M{row}"].Text = advance.Entry;
		worksheet.Range[$"N{row}"].Text = advance.SlipDT;
		worksheet.Range[$"O{row}"].Number = advance.Total;
	}

	private static void FillAdvanceTotals(IWorksheet worksheet, IEnumerable<AdvancePaymentModeTotalsModel> advancePaymentModeTotals)
	{
		string[] headers = ["Payment Mode", "Amount"];
		_rowCount = CommonExecl.FillData(worksheet, headers, _rowCount, advancePaymentModeTotals, FillAdvancePaymentModeRow);
	}

	private static void FillAdvancePaymentModeRow(IWorksheet worksheet, AdvancePaymentModeTotalsModel advance, int row)
	{
		worksheet.Range[$"A{row}"].Text = advance.PaymentMode;
		worksheet.Range[$"B{row}"].Number = advance.Amount;
	}
}
