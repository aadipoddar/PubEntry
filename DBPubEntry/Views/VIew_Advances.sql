CREATE VIEW View_Advances
	AS
	SELECT
		at.LocationId,
		at.Id Adv_Id,
		pt.Name Name,
		pt.Number Number,
		CASE WHEN pt.Loyalty = 1 THEN 'L' ELSE 'N' END Loyalty,
		at.DateTime Adv_Pymt_DT,
		at.AdvanceDate Adv_For_DT,
		at.ApprovedBy Remarks,
		at.Booking Booking_Amt,
		SUM(adt.Amount) Adv_Paid,
		STRING_AGG(pmt.Name, ', ') Pay_Mode,
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(at.TransactionId AS VARCHAR)
		END Slip_No,
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(ISNULL(tt.Cash, 0) + ISNULL(tt.Card, 0) + ISNULL(tt.UPI, 0) + ISNULL(tt.Amex, 0) AS VARCHAR)
		END Entry_Paid,
		CASE 
		    WHEN at.TransactionId = 0 THEN 'NOT REDEEMED' 
		    ELSE CAST(tt.DateTime AS VARCHAR)
		END Slip_DT,
		ISNULL(tt.Cash, 0) + ISNULL(tt.Card, 0) + ISNULL(tt.UPI, 0) + ISNULL(tt.Amex, 0) + SUM(adt.Amount) Total_Amt
	FROM Advance at
	LEFT JOIN AdvanceDetail adt ON at.Id = adt.AdvanceId
	LEFT JOIN PaymentMode pmt ON adt.[PaymentModeId] = pmt.Id
	LEFT JOIN Person pt ON at.PersonId = pt.Id
	LEFT JOIN [Transaction] tt ON at.TransactionId = tt.Id
	GROUP BY at.Id, pt.Name, pt.Number, pt.Loyalty, at.DateTime, at.AdvanceDate, at.ApprovedBy, at.Booking, at.TransactionId, tt.DateTime, tt.Cash, tt.Card, tt.UPI, tt.Amex, at.LocationId