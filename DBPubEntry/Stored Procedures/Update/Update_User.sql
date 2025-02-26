CREATE PROCEDURE Update_User
	@Id INT,
	@Name VARCHAR(100),
    @Password VARCHAR(100),
    @LocationId INT,
    @Admin BIT,
    @Status BIT
AS
BEGIN

    UPDATE [User]
    SET
        Name = @Name,
        Password = @Password,
        LocationId = @LocationId,
        Admin = @Admin,
        Status = @Status
    WHERE Id = @Id

END;