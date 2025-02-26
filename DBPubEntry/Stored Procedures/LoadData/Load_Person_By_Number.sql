CREATE PROCEDURE Load_Person_By_Number
	@Number VARCHAR(20)
AS
BEGIN

	SELECT * 
	FROM Person
	WHERE Number = ISNULL(@Number, '')

END;