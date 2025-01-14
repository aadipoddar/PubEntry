CREATE PROCEDURE Load_AdvanceTotals_By_ForDate_Location
	@FromDate DATETIME,
    @ToDate DATETIME,
    @LocationId INT
AS
BEGIN

    SELECT
        TotalBooking,
        RedeemedBooking,
        NotRedeemedBooking,
        TotalAdvance,
        RedeemedAdvance,
        NotRedeemedAdvance
    FROM View_AdvanceTotals
    WHERE AdvanceDate BETWEEN @FromDate AND @ToDate
		AND LocationId = @LocationId

END