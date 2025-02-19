namespace PubEntryLibrary.Printing.PDF;

public class PDF
{
	public static async Task<MemoryStream> Summary(DateTime fromDateTime, DateTime toDateTime) =>
		await SummaryPDF.Print(fromDateTime, toDateTime);

	public static async Task<MemoryStream> Detail(DateTime fromDateTime, DateTime toDateTime, int locationId) =>
		await DetailPDF.Print(fromDateTime, toDateTime, locationId);

	public static async Task<MemoryStream> AdvanceTakeOn(DateTime takenOn, int locationId) =>
		await AdvanceTakeOnPDF.Print(takenOn, locationId);
}
