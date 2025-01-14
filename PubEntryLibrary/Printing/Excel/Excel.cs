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
		(await CommonData.LoadTableDataById<LocationModel>(Table.Location, locationId)).Name;

	public static int SetupHeader(IWorksheet worksheet, string dateHeader, string locationName, string detailsHeader)
	{
		int row = 1;

		worksheet.Range[$"A{row}:M{row}"].Merge();
		worksheet.Range[$"A{row}"].Text = dateHeader;
		worksheet.Range[$"A{row}"].CellStyle.Font.Bold = true;
		worksheet.Range[$"A{row}"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range[$"A{row}"].CellStyle.Font.Size = 25;
		worksheet.Range[$"A{row}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range[$"A{row}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
		worksheet.Range[$"A{row}"].RowHeight = 47;

		row++;

		worksheet.Range[$"G{row}:I{row}"].Merge();
		worksheet.Range[$"G{row}"].Text = locationName;
		worksheet.Range[$"G{row}"].CellStyle.Font.Bold = true;
		worksheet.Range[$"G{row}"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range[$"G{row}"].CellStyle.Font.Size = 25;
		worksheet.Range[$"G{row}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range[$"G{row}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
		worksheet.Range[$"A{row}"].RowHeight = 15;

		row++;

		worksheet.Range[$"E{row}:I{row}"].Merge();
		worksheet.Range[$"E{row}"].Text = detailsHeader;
		worksheet.Range[$"E{row}"].CellStyle.Font.Bold = true;
		worksheet.Range[$"E{row}"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
		worksheet.Range[$"E{row}"].CellStyle.Font.Size = 25;
		worksheet.Range[$"E{row}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		worksheet.Range[$"E{row}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
		worksheet.Range[$"A{row}"].RowHeight = 15;

		return ++row;
	}

	public static int FillData<T>(IWorksheet worksheet, string[] headers, int startRow, IEnumerable<T> data, Action<IWorksheet, T, int> fillRowAction)
	{
		for (int i = 0; i < headers.Length; i++)
		{
			worksheet.Range[startRow, i + 1].Text = headers[i];
			worksheet.Range[startRow, i + 1].ColumnWidth = 12;
			worksheet.Range[startRow, i + 1].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
			worksheet.Range[startRow, i + 1].CellStyle.Font.Color = ExcelKnownColors.White;
			worksheet.Range[startRow, i + 1].CellStyle.Font.Bold = true;
		}

		foreach (var item in data)
			fillRowAction(worksheet, item, ++startRow);

		SetColumnAlignments(worksheet);
		return ++startRow;
	}

	public static void SetColumnAlignments(IWorksheet worksheet)
	{
		ExcelHAlign[] alignments = [
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignLeft,
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignRight,
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignRight
		];

		for (int i = 0; i < worksheet.Columns.Length && i < alignments.Length; i++)
			worksheet.Columns[i].CellStyle.HorizontalAlignment = alignments[i];
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
