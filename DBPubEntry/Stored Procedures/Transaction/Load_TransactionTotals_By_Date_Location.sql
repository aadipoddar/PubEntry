CREATE PROCEDURE Load_TransactionTotals_By_Date_Location
    @FromDate DATETIME,
    @ToDate DATETIME,
    @LocationId INT
AS
BEGIN
    
	SELECT
		tt.LocationId,
		SUM(tt.Male) Male,
		SUM(tt.Female) Female,
		SUM(CASE WHEN pt.Loyalty = 1 THEN 1 ELSE 0 END) Loyalty,
		SUM(tt.Cash) Cash,
		SUM(tt.Card) Card,
		SUM(tt.UPI) UPI,
		SUM(tt.Amex) Amex
	FROM [Transaction] tt
	JOIN Person pt ON tt.PersonId = pt.Id
	WHERE DateTime BETWEEN @FromDate AND @ToDate
        AND LocationId = @LocationId
    GROUP BY tt.LocationId

END