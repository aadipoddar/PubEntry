CREATE PROCEDURE [dbo].[Laod_Transaction_By_Location_Person]
	@LocationId INT,
    @PersonNumber VARCHAR(20)
AS
BEGIN
	DECLARE @CurrentDateTimeOffset DATETIMEOFFSET = SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30');
	DECLARE @CurrentDate DATE = CAST(@CurrentDateTimeOffset AS DATE);
	DECLARE @PreviousDate DATE = DATEADD(DAY, -1, @CurrentDate);
	DECLARE @CurrentHour INT = DATEPART(HOUR, @CurrentDateTimeOffset);

	DECLARE @PubOpenHour INT = CAST((SELECT [Value] FROM Settings WHERE [Key] = 'PubOpenTime') AS INT);
	DECLARE @PubCloseHour INT = CAST((SELECT [Value] FROM Settings WHERE [Key] = 'PubCloseTime') AS INT);

	SELECT
		[tt].[Id],
		[tt].[PersonId],
		pt.[Name] AS PersonName,
		pt.[Number] AS PersonNumber,
		pt.[Loyalty] AS PersonLoyalty,
		[tt].[Male],
		[tt].[Female],
		[tt].[Cash],
		[tt].[Card],
		[tt].[UPI],
		[tt].[Amex],
		[tt].[OnlineQR],
		[tt].[DateTime],
		[tt].[LocationId]
	FROM [Transaction] tt
	JOIN Person pt ON tt.PersonId = pt.Id
	WHERE tt.LocationId = @LocationId
		AND pt.Number = @PersonNumber
		AND (
			(@CurrentHour >= @PubOpenHour AND tt.DateTime >= @CurrentDate) OR
			(@CurrentHour < @PubCloseHour AND tt.DateTime >= @PreviousDate)
		)
END