CREATE PROCEDURE Update_ReservationType
    @Id INT,
    @Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    UPDATE ReservationType
    SET
        Name = @Name,
        Status = @Status
    WHERE Id = @Id

END;