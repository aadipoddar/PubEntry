using PubEntryLibrary.Data;
using PubEntryLibrary.Models.Printing;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

public static class AdvanceWorksheet
{
	private static int _rowCount;

	public static async Task AddAdvanceWorksheet(IWorkbook workbook, string dateHeader, int selectedLocationId, DateTime fromDateTime, DateTime toDateTime)
	{
		IWorksheet worksheet = workbook.Worksheets[1];
		worksheet.Name = "Advance";
		worksheet.Clear();

		string locationName = await Excel.GetLocationName(selectedLocationId);
		Excel.SetupHeader(worksheet, dateHeader, locationName, "Advance Details");

		var detailedAdvancePrintModel = await PrintData.LoadAdvancesByDateAndLocation(fromDateTime, toDateTime, selectedLocationId);
		FillAdvanceData(worksheet, detailedAdvancePrintModel);
		FillAdvanceTotals(worksheet, detailedAdvancePrintModel);
	}

	private static void FillAdvanceData(IWorksheet worksheet, IEnumerable<DetailedAdvancePrintModel> detailedAdvancePrintModels)
	{
		string[] headers = { "Adv Id", "Name", "Number", "Loyalty", "Adv Pymt DT", "Adv For DT", "Remarks", "Booking Amt", "Adv Paid", "Pay Mode", "Slip No", "Entry Paid", "Slip DT", "Total Amt" };
		_rowCount = Excel.FillData(worksheet, headers, detailedAdvancePrintModels, FillAdvanceRow);
	}

	private static void FillAdvanceRow(IWorksheet worksheet, DetailedAdvancePrintModel advance, int row)
	{
		worksheet.Range[$"A{row}"].Number = advance.Adv_Id;
		worksheet.Range[$"B{row}"].Text = advance.Name;
		worksheet.Range[$"C{row}"].Number = long.Parse(advance.Number);
		worksheet.Range[$"D{row}"].Text = advance.Loyalty.ToString();
		worksheet.Range[$"E{row}"].DateTime = advance.Adv_Pymt_DT;
		worksheet.Range[$"F{row}"].DateTime = advance.Adv_For_DT;
		worksheet.Range[$"G{row}"].Text = advance.Remarks;
		worksheet.Range[$"H{row}"].Number = advance.Booking_Amt;
		worksheet.Range[$"I{row}"].Number = advance.Adv_Paid;
		worksheet.Range[$"J{row}"].Text = advance.Pay_Mode;
		worksheet.Range[$"K{row}"].Text = advance.Slip_No;
		worksheet.Range[$"L{row}"].Text = advance.Entry_Paid;
		worksheet.Range[$"M{row}"].Text = advance.Slip_DT;
		worksheet.Range[$"N{row}"].Number = advance.Total_Amt;
	}

	private static void FillAdvanceTotals(IWorksheet worksheet, IEnumerable<DetailedAdvancePrintModel> detailedAdvancePrintModels)
	{
		int totalRow = _rowCount + 5;

		Excel.SetTotalCell(worksheet, $"B{totalRow + 2}", "Total Advance :", 20, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"B{totalRow + 3}", "Redeemed :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"B{totalRow + 4}", "Not Redeemed :", 15, ExcelHAlign.HAlignCenter);

		Excel.SetTotalCell(worksheet, $"D{totalRow + 2}", detailedAdvancePrintModels.Sum(x => x.Adv_Paid), 20, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"D{totalRow + 3}", detailedAdvancePrintModels.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Adv_Paid), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"D{totalRow + 4}", detailedAdvancePrintModels.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Adv_Paid), 15, ExcelHAlign.HAlignRight);

		Excel.SetTotalCell(worksheet, $"I{totalRow + 2}", "Total Booking :", 20, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 3}", "Redeemed :", 15, ExcelHAlign.HAlignCenter);
		Excel.SetTotalCell(worksheet, $"I{totalRow + 4}", "Not Redeemed :", 15, ExcelHAlign.HAlignCenter);

		Excel.SetTotalCell(worksheet, $"K{totalRow + 2}", detailedAdvancePrintModels.Sum(x => x.Booking_Amt), 20, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 3}", detailedAdvancePrintModels.Where(x => x.Slip_No != "NOT REDEEMED").Sum(x => x.Booking_Amt), 15, ExcelHAlign.HAlignRight);
		Excel.SetTotalCell(worksheet, $"K{totalRow + 4}", detailedAdvancePrintModels.Where(x => x.Slip_No == "NOT REDEEMED").Sum(x => x.Booking_Amt), 15, ExcelHAlign.HAlignRight);
	}
}
