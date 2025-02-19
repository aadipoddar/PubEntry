CREATE PROCEDURE Load_Advances_By_TakenOn_Location
	@TakenOn DATE,
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
	WHERE PaymentDT BETWEEN @TakenOn AND DATEADD(DAY, 1, @TakenOn)
		AND LocationId = @LocationId
	
END