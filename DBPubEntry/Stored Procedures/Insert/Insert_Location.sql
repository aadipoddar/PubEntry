CREATE PROCEDURE Insert_Location
	@Id INT,
	@Name VARCHAR(50),
    @Status BIT
AS
BEGIN

    IF @Id = 0
    BEGIN
        INSERT INTO Location (Name, Status)
        VALUES (@Name, @Status)
    END

    ELSE
    BEGIN
        UPDATE Location
            SET
                Name = @Name,
                Status = @Status
            WHERE Id = @Id
    END

END;