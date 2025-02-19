using Syncfusion.XlsIO;

namespace PubEntryLibrary.Printing.Excel;

public static class Excel
{
	public static async Task<MemoryStream> TransactionAdvanceExcel(DateTime fromDateTime, DateTime toDateTime, int locationId)
	{
		string dateHeader = $"{fromDateTime} - {toDateTime}";

		MemoryStream ms = new();
		using (ExcelEngine excelEngine = new())
		{
			IApplication application = excelEngine.Excel;
			application.DefaultVersion = ExcelVersion.Xlsx;

			IWorkbook workbook = application.Workbooks.Create(2);
			await TransactionWorksheet.AddTransactionWorksheet(workbook, dateHeader, locationId, fromDateTime, toDateTime);

			if (toDateTime.TimeOfDay < TimeSpan.FromHours(17))
				await AdvanceWorksheet.AddAdvanceWorksheet(workbook, dateHeader, locationId, fromDateTime.Date, toDateTime.AddDays(-1).Date.AddHours(23).AddMinutes(59));
			else await AdvanceWorksheet.AddAdvanceWorksheet(workbook, dateHeader, locationId, fromDateTime.Date, toDateTime.Date);

			workbook.SaveAs(ms);
			ms.Position = 0;
		}

		return ms;
	}

	public static async Task<MemoryStream> AdvanceTakeOnExcel(DateTime takenOn, int locationId)
	{
		string dateHeader = takenOn.ToLongDateString();

		MemoryStream ms = new();
		using (ExcelEngine excelEngine = new())
		{
			IApplication application = excelEngine.Excel;
			application.DefaultVersion = ExcelVersion.Xlsx;

			IWorkbook workbook = application.Workbooks.Create(1);
			await AdvanceTakeOnWorksheet.AddAdvanceTakeOnWorksheet(workbook, dateHeader, locationId, takenOn);

			workbook.SaveAs(ms);
			ms.Position = 0;
		}

		return ms;
	}
}
