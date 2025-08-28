CREATE VIEW View_Advances
	AS
	SELECT
		at.LocationId,
		at.Id,
		pt.Name,
		pt.Number,
		CASE WHEN pt.Loyalty = 1 THEN 'L' ELSE 'N' END Loyalty,
		at.DateTime PaymentDT,
		at.AdvanceDate ForDT,
		at.ApprovedBy Remarks,
		ut.Name [User],
		at.Booking Booking,
		SUM(adt.Amount) Amount,
		STRING_AGG(pmt.Name, ', ') Mode,
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(at.TransactionId AS VARCHAR)
		END SlipId,
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(ISNULL(tt.Cash, 0) + ISNULL(tt.Card, 0) + ISNULL(tt.UPI, 0) + ISNULL(tt.Amex, 0) + ISNULL(tt.OnlineQR, 0) AS VARCHAR)
		END [Entry],
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(tt.DateTime AS VARCHAR)
		END SlipDT,
		ISNULL(tt.Cash, 0) + ISNULL(tt.Card, 0) + ISNULL(tt.UPI, 0) + ISNULL(tt.Amex, 0) + ISNULL(tt.OnlineQR, 0) + SUM(adt.Amount) Total
	FROM Advance at
	LEFT JOIN AdvanceDetail adt ON at.Id = adt.AdvanceId
	LEFT JOIN PaymentMode pmt ON adt.[PaymentModeId] = pmt.Id
	LEFT JOIN Person pt ON at.PersonId = pt.Id
	LEFT JOIN [Transaction] tt ON at.TransactionId = tt.Id
	LEFT JOIN [User] ut ON at.UserId = ut.Id
	GROUP BY at.Id, pt.Name, pt.Number, pt.Loyalty, at.DateTime, at.AdvanceDate, at.ApprovedBy, at.Booking, at.TransactionId, tt.DateTime, tt.Cash, tt.Card, tt.UPI, tt.Amex, tt.OnlineQR, at.LocationId, ut.Name