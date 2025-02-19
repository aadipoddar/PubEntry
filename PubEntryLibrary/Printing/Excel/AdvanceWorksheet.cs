using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

internal static class AdvanceWorksheet
{
	private static int _rowCount;

	internal static async Task AddAdvanceWorksheet(IWorkbook workbook, string dateHeader, int locationId, DateTime fromDateTime, DateTime toDateTime)
	{
		IWorksheet worksheet = workbook.Worksheets[1];
		worksheet.Name = "Advance";
		worksheet.Clear();

		string locationName = await CommonExecl.GetLocationName(locationId);
		_rowCount = CommonExecl.SetupHeader(worksheet, dateHeader, locationName, "Advance Details");

		var detailedAdvancePrintModel = await AdvanceData.LoadAdvancesByForDateLocation(fromDateTime, toDateTime, locationId);
		FillAdvanceData(worksheet, detailedAdvancePrintModel);
		FillAdvanceTotals(worksheet, detailedAdvancePrintModel);
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

	private static void FillAdvanceTotals(IWorksheet worksheet, IEnumerable<AdvancePrintModel> detailedAdvancePrintModels)
	{
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 2}", "Total Advance :", 20, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 3}", "Redeemed :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"B{_rowCount + 4}", "Not Redeemed :", 15, ExcelHAlign.HAlignCenter);

		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 2}", detailedAdvancePrintModels.Sum(x => x.Amount), 20, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 3}", detailedAdvancePrintModels.Where(x => x.SlipId != "NOT REDEEMED").Sum(x => x.Amount), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"D{_rowCount + 4}", detailedAdvancePrintModels.Where(x => x.SlipId == "NOT REDEEMED").Sum(x => x.Amount), 15, ExcelHAlign.HAlignRight);

		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 2}", "Total Booking :", 20, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 3}", "Redeemed :", 15, ExcelHAlign.HAlignCenter);
		CommonExecl.SetTotalCell(worksheet, $"I{_rowCount + 4}", "Not Redeemed :", 15, ExcelHAlign.HAlignCenter);

		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 2}", detailedAdvancePrintModels.Sum(x => x.Booking), 20, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 3}", detailedAdvancePrintModels.Where(x => x.SlipId != "NOT REDEEMED").Sum(x => x.Booking), 15, ExcelHAlign.HAlignRight);
		CommonExecl.SetTotalCell(worksheet, $"K{_rowCount + 4}", detailedAdvancePrintModels.Where(x => x.SlipId == "NOT REDEEMED").Sum(x => x.Booking), 15, ExcelHAlign.HAlignRight);
	}
}
