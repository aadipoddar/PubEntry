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
        Number = @Number,
        Loyalty = @Loyalty
    WHERE Id = @Id;

END;