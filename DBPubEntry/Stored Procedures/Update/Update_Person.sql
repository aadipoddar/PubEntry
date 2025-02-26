CREATE PROCEDURE Update_Person
    @Id INT,
    @Name VARCHAR(250),
    @Number VARCHAR(250),
    @Loyalty BIT
AS
BEGIN

    UPDATE Person
    SET
        Name = @Name,
        Loyalty = @Loyalty
	OUTPUT INSERTED.Id
    WHERE Number = @Number;

END;