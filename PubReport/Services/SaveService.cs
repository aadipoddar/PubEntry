namespace PubReport.Services;

public partial class SaveService
{
	//Method to save document as a file and view the saved document.
	public static partial void SaveAndView(string filename, string contentType, MemoryStream stream);
}