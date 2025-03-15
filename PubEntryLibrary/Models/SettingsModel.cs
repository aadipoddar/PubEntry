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
	public static string RefreshReportTimer => "RefreshReportTimer";

	public static string PageWidthThermal => "PageWidthThermal";
	public static string PagePaddingTopThermal => "PagePaddingTopThermal";
	public static string PagePaddingBottomThermal => "PagePaddingBottomThermal";
	public static string PagePaddingLeftThermal => "PagePaddingLeftThermal";
	public static string PagePaddingRightThermal => "PagePaddingRightThermal";

	public static string HeaderFontFamilyThermal => "HeaderFontFamilyThermal";
	public static string HeaderFontSizeThermal => "HeaderFontSizeThermal";
	public static string HeaderFontWeightThermal => "HeaderFontWeightThermal";
	public static string HeaderFontPaddingTopThermal => "HeaderFontPaddingTopThermal";
	public static string HeaderFontPaddingBottomThermal => "HeaderFontPaddingBottomThermal";
	public static string HeaderFontPaddingLeftThermal => "HeaderFontPaddingLeftThermal";
	public static string HeaderFontPaddingRightThermal => "HeaderFontPaddingRightThermal";

	public static string SubHeaderFontFamilyThermal => "SubHeaderFontFamilyThermal";
	public static string SubHeaderFontSizeThermal => "SubHeaderFontSizeThermal";
	public static string SubHeaderFontWeightThermal => "SubHeaderFontWeightThermal";
	public static string SubHeaderFontPaddingTopThermal => "SubHeaderFontPaddingTopThermal";
	public static string SubHeaderFontPaddingBottomThermal => "SubHeaderFontPaddingBottomThermal";
	public static string SubHeaderFontPaddingLeftThermal => "SubHeaderFontPaddingLeftThermal";
	public static string SubHeaderFontPaddingRightThermal => "SubHeaderFontPaddingRightThermal";

	public static string RegularFontFamilyThermal => "RegularFontFamilyThermal";
	public static string RegularFontSizeThermal => "RegularFontSizeThermal";
	public static string RegularFontWeightThermal => "RegularFontWeightThermal";
	public static string RegularFontPaddingTopThermal => "RegularFontPaddingTopThermal";
	public static string RegularFontPaddingBottomThermal => "RegularFontPaddingBottomThermal";
	public static string RegularFontPaddingLeftThermal => "RegularFontPaddingLeftThermal";
	public static string RegularFontPaddingRightThermal => "RegularFontPaddingRightThermal";

	public static string FooterFontFamilyThermal => "FooterFontFamilyThermal";
	public static string FooterFontSizeThermal => "FooterFontSizeThermal";
	public static string FooterFontWeightThermal => "FooterFontWeightThermal";
	public static string FooterFontPaddingTopThermal => "FooterFontPaddingTopThermal";
	public static string FooterFontPaddingBottomThermal => "FooterFontPaddingBottomThermal";
	public static string FooterFontPaddingLeftThermal => "FooterFontPaddingLeftThermal";
	public static string FooterFontPaddingRightThermal => "FooterFontPaddingRightThermal";

	public static string SeparatorFontFamilyThermal => "SeparatorFontFamilyThermal";
	public static string SeparatorFontSizeThermal => "SeparatorFontSizeThermal";
	public static string SeparatorFontWeightThermal => "SeparatorFontWeightThermal";
	public static string SeparatorFontPaddingTopThermal => "SeparatorFontPaddingTopThermal";
	public static string SeparatorFontPaddingBottomThermal => "SeparatorFontPaddingBottomThermal";
	public static string SeparatorFontPaddingLeftThermal => "SeparatorFontPaddingLeftThermal";
	public static string SeparatorFontPaddingRightThermal => "SeparatorFontPaddingRightThermal";
	public static string SeparatorDashCountThermal => "SeparatorDashCountThermal";

	public static string FooterLine1 => "FooterLine1";
	public static string FooterLine2 => "FooterLine2";
}