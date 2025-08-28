﻿CREATE PROCEDURE Insert_PaymentMode
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    IF @Id = 0
    BEGIN
        INSERT INTO PaymentMode (Name, Status)
        VALUES (@Name, @Status)
    END

    ELSE
    BEGIN
        UPDATE PaymentMode
            SET
                Name = @Name,
                Status = @Status
            WHERE Id = @Id
    END

END;