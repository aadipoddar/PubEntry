CREATE PROCEDURE Load_Users_By_LocationId
	@LocationId INT
AS
BEGIN

	SELECT * FROM [User]
	WHERE LocationId = @LocationId
	AND Status = 1

END;