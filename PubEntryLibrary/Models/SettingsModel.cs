namespace PubEntryLibrary.Models;

public class SettingsModel
{
	public int Id { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }
}

public static class SettingsKeys
{
	public static string PubOpenTime => "PubOpenTime";
	public static string PubCloseTime => "PubCloseTime";
	public static string InactivityTime => "InactivityTime";
	public static string HeaderFontFamilyThermal => "HeaderFontFamilyThermal";
	public static string HeaderFontSizeThermal => "HeaderFontSizeThermal";
	public static string HeaderFontStyleThermal => "HeaderFontStyleThermal";
	public static string SubHeaderFontFamilyThermal => "SubHeaderFontFamilyThermal";
	public static string SubHeaderFontSizeThermal => "SubHeaderFontSizeThermal";
	public static string SubHeaderFontStyleThermal => "SubHeaderFontStyleThermal";
	public static string RegularFontFamilyThermal => "RegularFontFamilyThermal";
	public static string RegularFontSizeThermal => "RegularFontSizeThermal";
	public static string RegularFontStyleThermal => "RegularFontStyleThermal";
	public static string FooterFontFamilyThermal => "FooterFontFamilyThermal";
	public static string FooterFontSizeThermal => "FooterFontSizeThermal";
	public static string FooterFontStyleThermal => "FooterFontStyleThermal";
}