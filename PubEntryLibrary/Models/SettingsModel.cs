namespace PubEntryLibrary.Models;

public class SettingsModel
{
	public int Id { get; set; }

	public int InactivityTime { get; set; }

	public string HeaderFontFamilyThermal { get; set; }
	public int HeaderFontSizeThermal { get; set; }
	public int HeaderFontStyleThermal { get; set; }

	public string SubHeaderFontFamilyThermal { get; set; }
	public int SubHeaderFontSizeThermal { get; set; }
	public int SubHeaderFontStyleThermal { get; set; }

	public string RegularFontFamilyThermal { get; set; }
	public int RegularFontSizeThermal { get; set; }
	public int RegularFontStyleThermal { get; set; }

	public string FooterFontFamilyThermal { get; set; }
	public int FooterFontSizeThermal { get; set; }
	public int FooterFontStyleThermal { get; set; }
}