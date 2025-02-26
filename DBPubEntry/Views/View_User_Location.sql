CREATE VIEW [dbo].[View_User_Location]
	AS
	SELECT
		ut.Id,
		ut.Name,
		ut.Password,
		lt.Id LocationId,
		lt.Name LocationName,
		ut.Admin,
		ut.Status
	FROM [User] ut
	LEFT JOIN Location lt ON ut.LocationId = lt.Id
