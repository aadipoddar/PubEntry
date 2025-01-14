CREATE PROCEDURE Load_TransactionTotals_By_Date_Location
    @FromDate DATETIME,
    @ToDate DATETIME,
    @LocationId INT
AS
BEGIN
    
	SELECT
		Male,
		Female,
		Loyalty,
		Cash,
		Card,
		UPI,
		Amex
	FROM View_TransactionTotals
    WHERE DateTime BETWEEN @FromDate AND @ToDate
        AND LocationId = @LocationId

END