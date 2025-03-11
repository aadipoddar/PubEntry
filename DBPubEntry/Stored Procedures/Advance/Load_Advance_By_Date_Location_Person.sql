CREATE PROCEDURE Load_Advance_By_Date_Location_Person
    @LocationId INT,
    @PersonId INT,
    @AdvanceDate DATE
AS
BEGIN
    DECLARE @CurrentDateTimeOffset DATETIMEOFFSET = SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30');
    DECLARE @CurrentDate DATE = CAST(@CurrentDateTimeOffset AS DATE);
    DECLARE @PreviousDate DATE = DATEADD(DAY, -1, @CurrentDate);
    DECLARE @CurrentHour INT = DATEPART(HOUR, @CurrentDateTimeOffset);

    DECLARE @PubOpenHour INT = CAST((SELECT [Value] FROM Settings WHERE [Key] = 'PubOpenTime') AS INT);
    DECLARE @PubCloseHour INT = CAST((SELECT [Value] FROM Settings WHERE [Key] = 'PubCloseTime') AS INT);

    SELECT
        at.*
    FROM Advance at
    WHERE at.LocationId = @LocationId
        AND at.PersonId = @PersonId
        AND at.TransactionId = 0
        AND (
            (@AdvanceDate IS NULL AND 
                (
                    (@CurrentHour >= @PubOpenHour AND at.AdvanceDate = @CurrentDate) OR
                    (@CurrentHour < @PubCloseHour AND at.AdvanceDate = @PreviousDate)
                )
            )
            OR (@AdvanceDate IS NOT NULL AND at.AdvanceDate = @AdvanceDate)
        )
END