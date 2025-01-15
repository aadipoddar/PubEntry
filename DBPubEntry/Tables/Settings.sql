CREATE TABLE [dbo].[Settings]
(
    [Id] INT NOT NULL, 
	[InactivityTime] INT NOT NULL DEFAULT 5,
    [HeaderFontFamilyThermal] VARCHAR(20) NOT NULL DEFAULT 'Arial', 
    [HeaderFontSizeThermal] INT NOT NULL DEFAULT 25, 
    [HeaderFontStyleThermal] INT NOT NULL DEFAULT 1, 
    [SubHeaderFontFamilyThermal] VARCHAR(20) NOT NULL DEFAULT 'Arial', 
    [SubHeaderFontSizeThermal] INT NOT NULL DEFAULT 15, 
    [SubHeaderFontStyleThermal] INT NOT NULL DEFAULT 1, 
    [RegularFontFamilyThermal] VARCHAR(20) NOT NULL DEFAULT 'Courier New', 
    [RegularFontSizeThermal] INT NOT NULL DEFAULT 12, 
    [RegularFontStyleThermal] INT NOT NULL DEFAULT 1, 
    [FooterFontFamilyThermal] VARCHAR(20) NOT NULL DEFAULT 'Courier New', 
    [FooterFontSizeThermal] INT NOT NULL DEFAULT 8, 
    [FooterFontStyleThermal] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Setting] PRIMARY KEY ([Id]) 
)
