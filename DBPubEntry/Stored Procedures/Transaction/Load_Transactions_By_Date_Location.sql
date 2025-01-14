CREATE PROCEDURE Load_Transactions_By_Date_Location
    @FromDate DATETIME,
    @ToDate DATETIME,
    @LocationId INT
AS
BEGIN

    SELECT
        *
    FROM View_Transactions
    WHERE DateTime BETWEEN @FromDate AND @ToDate
        AND LocationId = @LocationId

END