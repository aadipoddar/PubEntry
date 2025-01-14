CREATE PROCEDURE Load_Advances_By_ForDate_Location
	@FromDate DATETIME,
    @ToDate DATETIME,
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
	WHERE Adv_For_DT BETWEEN @FromDate AND @ToDate
		AND LocationId = @LocationId
	
END