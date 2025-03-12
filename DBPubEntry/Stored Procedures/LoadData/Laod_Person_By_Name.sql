CREATE PROCEDURE Load_Person_By_Name
	@Name VARCHAR(250)
AS
BEGIN

	SELECT * 
	FROM Person
	WHERE [Name] = ISNULL(@Name, '')

END;