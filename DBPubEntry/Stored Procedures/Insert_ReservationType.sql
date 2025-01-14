CREATE PROCEDURE Insert_ReservationType
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    INSERT INTO ReservationType(
        Name,
        Status
    )
    VALUES (
        @Name,
        @Status
    )

END;