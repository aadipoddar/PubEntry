CREATE PROCEDURE Insert_Location
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    INSERT INTO Location (
        Name,
        Status
    )
    VALUES (
        @Name,
        @Status
    )

END;