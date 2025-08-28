CREATE PROCEDURE Insert_ReservationType
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    IF @Id = 0
    BEGIN
        INSERT INTO ReservationType (Name, Status)
        VALUES (@Name, @Status)
    END

    ELSE
    BEGIN
        UPDATE ReservationType
            SET
                Name = @Name,
                Status = @Status
            WHERE Id = @Id
    END

END;