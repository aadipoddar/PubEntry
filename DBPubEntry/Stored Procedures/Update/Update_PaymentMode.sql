CREATE PROCEDURE Update_PaymentMode
    @Id INT,
    @Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    UPDATE PaymentMode
    SET
        Name = @Name,
        Status = @Status
    WHERE Id = @Id

END;