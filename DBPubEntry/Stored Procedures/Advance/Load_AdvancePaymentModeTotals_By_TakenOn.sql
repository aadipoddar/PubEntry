CREATE PROCEDURE Load_AdvancePaymentModeTotals_By_TakenOn
	@TakenOn DATE,
    @LocationId INT
AS
BEGIN

	SELECT
		pmt.Name PaymentMode,
		SUM(adt.Amount) Amount
	FROM AdvanceDetail adt
	JOIN PaymentMode pmt ON adt.[PaymentModeId] = pmt.Id
	JOIN Advance at ON adt.AdvanceId = at.Id
	WHERE DateTime BETWEEN @TakenOn AND DATEADD(DAY, 1, @TakenOn)
		AND LocationId = @LocationId
	GROUP BY adt.[PaymentModeId], pmt.Name

END