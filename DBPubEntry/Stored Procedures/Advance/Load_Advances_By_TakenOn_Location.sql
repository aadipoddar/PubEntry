CREATE PROCEDURE Load_Advances_By_TakenOn_Location
	@TakenOn DATE,
    @LocationId INT
AS
BEGIN

	SELECT
		Adv_Id,
		Name,
		Number,
		Loyalty,
		Adv_Pymt_DT,
		Adv_For_DT,
		Remarks,
		Booking_Amt,
		Adv_Paid,
		Pay_Mode,
		Slip_No,
		Entry_Paid,
		Slip_DT,
		Total_Amt
	FROM View_Advances
	WHERE Adv_Pymt_DT BETWEEN @TakenOn AND DATEADD(DAY, 1, @TakenOn)
		AND LocationId = @LocationId
	
END