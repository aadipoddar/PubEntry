CREATE PROCEDURE Insert_PaymentMode
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    INSERT INTO PaymentMode (
        Name,
        Status
    ) VALUES (
        @Name,
        @Status
    )

END;