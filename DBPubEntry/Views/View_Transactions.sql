CREATE VIEW [dbo].[View_Transactions]
	AS
	SELECT
        tt.Id,
        tt.LocationId,
        lt.Name LocationName,
        pt.Name PersonName,
        pt.Number PersonNumber,
        CASE WHEN pt.Loyalty = 1 THEN 'L' ELSE 'N' END Loyalty,
        rt.Name Reservation,
        tt.Male,
        tt.Female,
        tt.Cash,
        tt.Card,
        tt.UPI,
        tt.Amex,
        tt.OnlineQR,
        tt.ApprovedBy,
        ut.Name EnteredBy,
        tt.DateTime
    FROM [Transaction] tt
    JOIN Location lt ON tt.LocationId = lt.Id
    JOIN Person pt ON tt.PersonId = pt.Id
    LEFT JOIN ReservationType rt ON tt.[ReservationTypeId] = rt.Id
    JOIN [User] ut ON tt.UserId = ut.Id