CREATE PROCEDURE Load_Advances_By_ForDate_Location
	@FromDate DATETIME,
    @ToDate DATETIME,
    @LocationId INT
AS
BEGIN

	SELECT
		Id,
		Name,
		Number,
		Loyalty,
		PaymentDT,
		ForDT,
		Remarks,
		[User],
		Booking,
		Amount,
		Mode,
		SlipId,
		[Entry],
		SlipDT,
		Total
	FROM View_Advances
	WHERE ForDT BETWEEN @FromDate AND @ToDate
		AND LocationId = @LocationId
	
END