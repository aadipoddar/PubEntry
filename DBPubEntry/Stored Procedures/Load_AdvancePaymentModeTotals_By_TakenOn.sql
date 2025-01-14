CREATE PROCEDURE Load_AdvancePaymentModeTotals_By_TakenOn
	@TakenOn DATE,
    @LocationId INT
AS
BEGIN

	SELECT
		PaymentMode,
		Amount
	FROM View_AdvancePaymentModeTotals
	WHERE DateTime BETWEEN @TakenOn AND DATEADD(DAY, 1, @TakenOn)
		AND LocationId = @LocationId

END