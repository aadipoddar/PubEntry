CREATE PROCEDURE Insert_User
	@Id INT,
	@Name VARCHAR(100),
    @Password VARCHAR(100),
    @LocationId INT,
    @Admin BIT,
    @Status BIT
AS
BEGIN

    IF @Id = 0
    BEGIN
        INSERT INTO [User] (
            Name,
            Password,
            LocationId,
            Admin,
            Status)
        VALUES (
            @Name,
            @Password,
            @LocationId,
            @Admin,
            @Status)
    END

    ELSE
    BEGIN
        UPDATE [User]
        SET
            Name = @Name,
            Password = @Password,
            LocationId = @LocationId,
            Admin = @Admin,
            Status = @Status
        WHERE Id = @Id
    END

END