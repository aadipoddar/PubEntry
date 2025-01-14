CREATE VIEW View_AdvancePaymentModeTotals
	AS
	SELECT
		at.LocationId,
		at.DateTime,
		pmt.Name PaymentMode,
		SUM(adt.Amount) Amount
	FROM AdvanceDetail adt
	JOIN PaymentMode pmt ON adt.[PaymentModeId] = pmt.Id
	JOIN Advance at ON adt.AdvanceId = at.Id
	GROUP BY adt.[PaymentModeId], pmt.Name, at.LocationId, at.DateTime