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

    SELECT
        at.*
    FROM Advance at
    WHERE at.LocationId = @LocationId
        AND at.PersonId = @PersonId
        AND at.TransactionId = 0
        AND (
            (@AdvanceDate IS NULL AND 
                (
					--( @CurrentHour >= 17 AND at.AdvanceDate = @CurrentDate ) OR
                    ( @CurrentHour >= 5 AND at.AdvanceDate = @CurrentDate ) OR
                    ( @CurrentHour < 5 AND at.AdvanceDate = @PreviousDate)
                )
            )
            OR (@AdvanceDate IS NOT NULL AND at.AdvanceDate = @AdvanceDate)
        )

END