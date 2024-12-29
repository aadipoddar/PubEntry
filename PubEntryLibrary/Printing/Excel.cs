using PubEntryLibrary.Data;

using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing;

public static class Excel
{
	public static async Task<MemoryStream> ExcelExport(string dateHeader, string fromTime, string toTime, int selectedLocationId)
	{
		MemoryStream ms = new();
		using (ExcelEngine excelEngine = new())
		{
			IApplication application = excelEngine.Excel;
			application.DefaultVersion = ExcelVersion.Xlsx;

			IWorkbook workbook = application.Workbooks.Create(1);
			IWorksheet worksheet = workbook.Worksheets[0];

			worksheet.IsGridLinesVisible = true;

			worksheet.Range["A1:M1"].Merge();

			worksheet.Range["A1"].Text = dateHeader;
			worksheet.Range["A1"].CellStyle.Font.Bold = true;
			worksheet.Range["A1"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
			worksheet.Range["A1"].CellStyle.Font.Size = 25;

			worksheet.Range["A1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Range["A1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

			worksheet.Range["H2:I2"].Merge();

			worksheet.Range["H2"].Text = $"{(await CommonData.LoadTableDataById<LocationModel>("LocationTable", selectedLocationId)).FirstOrDefault().Name}";
			worksheet.Range["H2"].CellStyle.Font.Bold = true;
			worksheet.Range["H2"].CellStyle.Font.RGBColor = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
			worksheet.Range["H2"].CellStyle.Font.Size = 25;

			worksheet.Range["H2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Range["H2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

			var detailedPrintModels = await PrintData.LoadTransactionsByDateAndLocation(fromTime, toTime, selectedLocationId);

			worksheet.Range["A4"].Text = "SlipId";
			worksheet.Range["B4"].Text = "Name";
			worksheet.Range["C4"].Text = "Number";
			worksheet.Range["D4"].Text = "Loyalty";
			worksheet.Range["E4"].Text = "Male";
			worksheet.Range["F4"].Text = "Female";
			worksheet.Range["G4"].Text = "Cash";
			worksheet.Range["H4"].Text = "Card";
			worksheet.Range["I4"].Text = "UPI";
			worksheet.Range["J4"].Text = "Amex";
			worksheet.Range["K4"].Text = "Approved By";
			worksheet.Range["L4"].Text = "Entered By";
			worksheet.Range["M4"].Text = "Date Time";

			worksheet.Range["A4:M4"].CellStyle.Color = Syncfusion.Drawing.Color.FromArgb(0, 42, 118, 189);
			worksheet.Range["A4:M4"].CellStyle.Font.Color = ExcelKnownColors.White;
			worksheet.Range["A4:M4"].CellStyle.Font.Bold = true;

			worksheet.Range["A1"].ColumnWidth = 8;
			worksheet.Range["B1"].ColumnWidth = 30;
			worksheet.Range["C1"].ColumnWidth = 15;
			worksheet.Range["D1"].ColumnWidth = 8;
			worksheet.Range["E1"].ColumnWidth = 8;
			worksheet.Range["F1"].ColumnWidth = 8;
			worksheet.Range["G1"].ColumnWidth = 15;
			worksheet.Range["H1"].ColumnWidth = 15;
			worksheet.Range["I1"].ColumnWidth = 15;
			worksheet.Range["J1"].ColumnWidth = 8;
			worksheet.Range["K1"].ColumnWidth = 20;
			worksheet.Range["L1"].ColumnWidth = 20;
			worksheet.Range["M1"].ColumnWidth = 30;

			worksheet.Range["A1"].RowHeight = 47;
			worksheet.Range["A2"].RowHeight = 15;
			worksheet.Range["A4"].RowHeight = 15;

			int rowCount = 1;
			foreach (var transaction in detailedPrintModels)
			{
				worksheet.Range[$"A{rowCount + 4}"].Number = transaction.SlipId;
				worksheet.Range[$"B{rowCount + 4}"].Text = transaction.PersonName;
				worksheet.Range[$"C{rowCount + 4}"].Number = long.Parse(transaction.PersonNumber);
				worksheet.Range[$"D{rowCount + 4}"].Text = transaction.Loyalty.ToString();
				worksheet.Range[$"E{rowCount + 4}"].Number = transaction.Male;
				worksheet.Range[$"F{rowCount + 4}"].Number = transaction.Female;
				worksheet.Range[$"G{rowCount + 4}"].Number = transaction.Cash;
				worksheet.Range[$"H{rowCount + 4}"].Number = transaction.Card;
				worksheet.Range[$"I{rowCount + 4}"].Number = transaction.UPI;
				worksheet.Range[$"J{rowCount + 4}"].Number = transaction.Amex;
				worksheet.Range[$"K{rowCount + 4}"].Text = transaction.ApprovedBy;
				worksheet.Range[$"L{rowCount + 4}"].Text = transaction.EnteredBy;
				worksheet.Range[$"M{rowCount + 4}"].Text = transaction.ReceiptDate.ToString("dd/MM/yy HH:mm");

				rowCount++;
			}

			worksheet.Columns[0].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Columns[1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
			worksheet.Columns[2].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Columns[3].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Columns[4].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[5].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[6].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[7].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[8].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[9].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
			worksheet.Columns[10].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Columns[11].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
			worksheet.Columns[12].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			#region Total

			worksheet.Range[$"B{rowCount + 7}"].Text = "Total People :";
			worksheet.Range[$"B{rowCount + 7}"].CellStyle.Font.Size = 20;
			worksheet.Range[$"B{rowCount + 7}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"B{rowCount + 7}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"B{rowCount + 8}"].Text = "Total Male :";
			worksheet.Range[$"B{rowCount + 8}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"B{rowCount + 8}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"B{rowCount + 8}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"B{rowCount + 9}"].Text = "Total Female :";
			worksheet.Range[$"B{rowCount + 9}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"B{rowCount + 9}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"B{rowCount + 9}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"B{rowCount + 10}"].Text = "Total Loyalty :";
			worksheet.Range[$"B{rowCount + 10}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"B{rowCount + 10}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"B{rowCount + 10}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"C{rowCount + 7}"].Number = detailedPrintModels.Sum(x => x.Male) + detailedPrintModels.Sum(x => x.Female);
			worksheet.Range[$"C{rowCount + 7}"].CellStyle.Font.Size = 20;
			worksheet.Range[$"C{rowCount + 7}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"C{rowCount + 7}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"C{rowCount + 8}"].Number = detailedPrintModels.Sum(x => x.Male);
			worksheet.Range[$"C{rowCount + 8}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"C{rowCount + 8}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"C{rowCount + 8}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"C{rowCount + 9}"].Number = detailedPrintModels.Sum(x => x.Female);
			worksheet.Range[$"C{rowCount + 9}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"C{rowCount + 9}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"C{rowCount + 9}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"C{rowCount + 10}"].Number = detailedPrintModels.Sum(x => x.Loyalty);
			worksheet.Range[$"C{rowCount + 10}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"C{rowCount + 10}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"C{rowCount + 10}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"I{rowCount + 7}"].Text = "Total Amount :";
			worksheet.Range[$"I{rowCount + 7}"].CellStyle.Font.Size = 20;
			worksheet.Range[$"I{rowCount + 7}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"I{rowCount + 7}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"I{rowCount + 8}"].Text = "Total Cash :";
			worksheet.Range[$"I{rowCount + 8}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"I{rowCount + 8}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"I{rowCount + 8}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"I{rowCount + 9}"].Text = "Total Card :";
			worksheet.Range[$"I{rowCount + 9}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"I{rowCount + 9}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"I{rowCount + 9}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"I{rowCount + 10}"].Text = "Total UPI :";
			worksheet.Range[$"I{rowCount + 10}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"I{rowCount + 10}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"I{rowCount + 10}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"I{rowCount + 11}"].Text = "Total Amex :";
			worksheet.Range[$"I{rowCount + 11}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"I{rowCount + 11}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"I{rowCount + 11}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

			worksheet.Range[$"K{rowCount + 7}"].Number = detailedPrintModels.Sum(x => x.Cash) + detailedPrintModels.Sum(x => x.Card) + detailedPrintModels.Sum(x => x.UPI) + detailedPrintModels.Sum(x => x.Amex);
			worksheet.Range[$"K{rowCount + 7}"].CellStyle.Font.Size = 20;
			worksheet.Range[$"K{rowCount + 7}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"K{rowCount + 7}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"K{rowCount + 8}"].Number = detailedPrintModels.Sum(x => x.Cash);
			worksheet.Range[$"K{rowCount + 8}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"K{rowCount + 8}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"K{rowCount + 8}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"K{rowCount + 9}"].Number = detailedPrintModels.Sum(x => x.Card);
			worksheet.Range[$"K{rowCount + 9}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"K{rowCount + 9}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"K{rowCount + 9}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"K{rowCount + 10}"].Number = detailedPrintModels.Sum(x => x.UPI);
			worksheet.Range[$"K{rowCount + 10}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"K{rowCount + 10}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"K{rowCount + 10}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			worksheet.Range[$"K{rowCount + 11}"].Number = detailedPrintModels.Sum(x => x.Amex);
			worksheet.Range[$"K{rowCount + 11}"].CellStyle.Font.Size = 15;
			worksheet.Range[$"K{rowCount + 11}"].CellStyle.Font.Bold = true;
			worksheet.Range[$"K{rowCount + 11}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;

			#endregion

			workbook.SaveAs(ms);
			ms.Position = 0;
		}
		return ms;
	}
}
