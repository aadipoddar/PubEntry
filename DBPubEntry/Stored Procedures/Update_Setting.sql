CREATE PROCEDURE Update_Settings
	@Id INT,
	@InactivityTime INT,

	@HeaderFontFamilyThermal VARCHAR(20),
	@HeaderFontSizeThermal INT,
	@HeaderFontStyleThermal INT,

	@SubHeaderFontFamilyThermal VARCHAR(20),
	@SubHeaderFontSizeThermal INT,
	@SubHeaderFontStyleThermal INT,

	@RegularFontFamilyThermal VARCHAR(20),
	@RegularFontSizeThermal INT,
	@RegularFontStyleThermal INT,

	@FooterFontFamilyThermal VARCHAR(20),
	@FooterFontSizeThermal INT,
	@FooterFontStyleThermal INT
AS
BEGIN

	UPDATE Settings
	SET
		InactivityTime = @InactivityTime,

		HeaderFontFamilyThermal = @HeaderFontFamilyThermal,
		HeaderFontSizeThermal = @HeaderFontSizeThermal,
		HeaderFontStyleThermal = @HeaderFontStyleThermal,

		SubHeaderFontFamilyThermal = @SubHeaderFontFamilyThermal,
		SubHeaderFontSizeThermal = @SubHeaderFontSizeThermal,
		SubHeaderFontStyleThermal = @SubHeaderFontStyleThermal,

		RegularFontFamilyThermal = @RegularFontFamilyThermal,
		RegularFontSizeThermal = @RegularFontSizeThermal,
		RegularFontStyleThermal = @RegularFontStyleThermal,

		FooterFontFamilyThermal = @FooterFontFamilyThermal,
		FooterFontSizeThermal = @FooterFontSizeThermal,
		FooterFontStyleThermal = @FooterFontStyleThermal
	WHERE Id = 1

END