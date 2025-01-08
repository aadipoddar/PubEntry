using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

public static class Excel
{
	public static async Task<MemoryStream> ExcelExport(DateTime fromDateTime, DateTime toDateTime, int selectedLocationId)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

		MemoryStream ms = new();
		using (ExcelEngine excelEngine = new())
		{
			IApplication application = excelEngine.Excel;
			application.DefaultVersion = ExcelVersion.Xlsx;

			IWorkbook workbook = application.Workbooks.Create(2);
			await TransactionWorksheet.AddTransactionWorksheet(workbook, dateHeader, selectedLocationId, fromDateTime, toDateTime);

			if (toDateTime.TimeOfDay < TimeSpan.FromHours(17))
				await AdvanceWorksheet.AddAdvanceWorksheet(workbook, dateHeader, selectedLocationId, fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59));
			else await AdvanceWorksheet.AddAdvanceWorksheet(workbook, dateHeader, selectedLocationId, fromDateTime.Date, toDateTime.Date);

			workbook.SaveAs(ms);
			ms.Position = 0;
		}
		return ms;
	}

	public static async Task<string> GetLocationName(int locationId) =>
		(await CommonData.LoadTableDataById<LocationModel>("LocationTable", locationId)).FirstOrDefault()?.Name ?? string.Empty;

	public static void SetupHeader(IWorksheet worksheet, string dateHeader, string locationName, string detailsHeader)
	{
		worksheet.Range["A1:M1"].Merge();
		worksheet.Range["A1"].Text = dateHeader;
		worksheet.Range["A1"].CellStyle.Font.Bold = true;
		worksheet.Range["A1"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range["A1"].CellStyle.Font.Size = 25;
		worksheet.Range["A1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range["A1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

		worksheet.Range["G2:I2"].Merge();
		worksheet.Range["G2"].Text = locationName;
		worksheet.Range["G2"].CellStyle.Font.Bold = true;
		worksheet.Range["G2"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range["G2"].CellStyle.Font.Size = 25;
		worksheet.Range["G2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range["G2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

		worksheet.Range["E3:I3"].Merge();
		worksheet.Range["E3"].Text = detailsHeader;
		worksheet.Range["E3"].CellStyle.Font.Bold = true;
		worksheet.Range["E3"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range["E3"].CellStyle.Font.Size = 25;
		worksheet.Range["E3"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range["E3"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

		worksheet.Range["A1"].RowHeight = 47;
		worksheet.Range["A2"].RowHeight = 15;
		worksheet.Range["A4"].RowHeight = 15;
	}

	public static int FillData<T>(IWorksheet worksheet, string[] headers, IEnumerable<T> data, Action<IWorksheet, T, int> fillRowAction)
	{
		for (int i = 0; i < headers.Length; i++)
		{
			worksheet.Range[4, i + 1].Text = headers[i];
			worksheet.Range[4, i + 1].ColumnWidth = 12;
			worksheet.Range[4, i + 1].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
			worksheet.Range[4, i + 1].CellStyle.Font.Color = ExcelKnownColors.White;
			worksheet.Range[4, i + 1].CellStyle.Font.Bold = true;
		}

		int rowCount = 1;
		foreach (var item in data)
		{
			fillRowAction(worksheet, item, rowCount + 4);
			rowCount++;
		}

		SetColumnAlignments(worksheet);

		return rowCount;
	}

	public static void SetColumnAlignments(IWorksheet worksheet)
	{
		int maxColumns = worksheet.Columns.Length;

		if (maxColumns > 0) worksheet.Columns[0].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		if (maxColumns > 1) worksheet.Columns[1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
		if (maxColumns > 2) worksheet.Columns[2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		if (maxColumns > 3) worksheet.Columns[3].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		if (maxColumns > 4) worksheet.Columns[4].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 5) worksheet.Columns[5].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 6) worksheet.Columns[6].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 7) worksheet.Columns[7].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 8) worksheet.Columns[8].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 9) worksheet.Columns[9].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 10) worksheet.Columns[10].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		if (maxColumns > 11) worksheet.Columns[11].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
		if (maxColumns > 12) worksheet.Columns[12].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		if (maxColumns > 13) worksheet.Columns[13].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
	}

	public static void SetTotalCell(IWorksheet worksheet, string cellAddress, string text, int fontSize, ExcelHAlign hAlign)
	{
		worksheet.Range[cellAddress].Text = text;
		worksheet.Range[cellAddress].CellStyle.Font.Size = fontSize;
		worksheet.Range[cellAddress].CellStyle.Font.Bold = true;
		worksheet.Range[cellAddress].CellStyle.HorizontalAlignment = hAlign;
	}

	public static void SetTotalCell(IWorksheet worksheet, string cellAddress, double number, int fontSize, ExcelHAlign hAlign)
	{
		worksheet.Range[cellAddress].Number = number;
		worksheet.Range[cellAddress].CellStyle.Font.Size = fontSize;
		worksheet.Range[cellAddress].CellStyle.Font.Bold = true;
		worksheet.Range[cellAddress].CellStyle.HorizontalAlignment = hAlign;
	}
}
