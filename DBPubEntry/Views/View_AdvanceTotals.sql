CREATE VIEW View_AdvanceTotals
	AS
	SELECT
        at.LocationId,
        at.AdvanceDate,
        SUM(at.Booking) AS TotalBooking,
        SUM(CASE WHEN at.TransactionId != 0 THEN at.Booking ELSE 0 END) AS RedeemedBooking,
        SUM(CASE WHEN at.TransactionId = 0 THEN at.Booking ELSE 0 END) AS NotRedeemedBooking,
        COALESCE(SUM(adt.AdvanceAmount), 0) AS TotalAdvance,
        COALESCE(SUM(CASE WHEN at.TransactionId != 0 THEN adt.AdvanceAmount ELSE 0 END), 0) AS RedeemedAdvance,
        COALESCE(SUM(CASE WHEN at.TransactionId = 0 THEN adt.AdvanceAmount ELSE 0 END), 0) AS NotRedeemedAdvance
    FROM Advance at
    LEFT JOIN (
        SELECT
            AdvanceId,
            SUM(Amount) AS AdvanceAmount
        FROM AdvanceDetail
        GROUP BY AdvanceId
    ) adt ON at.Id = adt.AdvanceId
    GROUP BY at.LocationId, at.AdvanceDate;