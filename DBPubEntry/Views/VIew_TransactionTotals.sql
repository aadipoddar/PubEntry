CREATE VIEW View_TransactionTotals
	AS
	SELECT
		tt.LocationId,
		tt.DateTime,
		SUM(tt.Male) Male,
		SUM(tt.Female) Female,
		SUM(CASE WHEN pt.Loyalty = 1 THEN 1 ELSE 0 END) Loyalty,
		SUM(tt.Cash) Cash,
		SUM(tt.Card) Card,
		SUM(tt.UPI) UPI,
		SUM(tt.Amex) Amex
	FROM [Transaction] tt
	JOIN Person pt ON tt.PersonId = pt.Id
    GROUP BY tt.LocationId, tt.DateTime
