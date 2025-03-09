using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

internal static class CommonExecl
{
	internal static async Task<string> GetLocationName(int locationId) =>
		(await CommonData.LoadTableDataById<LocationModel>(TableNames.Location, locationId)).Name;

	internal static int SetupHeader(IWorksheet worksheet, string dateHeader, string locationName, string detailsHeader)
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

	internal static int FillData<T>(IWorksheet worksheet, string[] headers, int startRow, IEnumerable<T> data, Action<IWorksheet, T, int> fillRowAction)
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

	internal static void SetColumnAlignments(IWorksheet worksheet)
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
			ExcelHAlign.HAlignCenter,
			ExcelHAlign.HAlignRight
		];

		for (int i = 0; i < worksheet.Columns.Length && i < alignments.Length; i++)
			worksheet.Columns[i].CellStyle.HorizontalAlignment = alignments[i];
	}

	internal static void SetTotalCell(IWorksheet worksheet, string cellAddress, string text, int fontSize, ExcelHAlign hAlign)
	{
		worksheet.Range[cellAddress].Text = text;
		worksheet.Range[cellAddress].CellStyle.Font.Size = fontSize;
		worksheet.Range[cellAddress].CellStyle.Font.Bold = true;
		worksheet.Range[cellAddress].CellStyle.HorizontalAlignment = hAlign;
	}

	internal static void SetTotalCell(IWorksheet worksheet, string cellAddress, double number, int fontSize, ExcelHAlign hAlign)
	{
		worksheet.Range[cellAddress].Number = number;
		worksheet.Range[cellAddress].CellStyle.Font.Size = fontSize;
		worksheet.Range[cellAddress].CellStyle.Font.Bold = true;
		worksheet.Range[cellAddress].CellStyle.HorizontalAlignment = hAlign;
	}
}