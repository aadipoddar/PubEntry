CREATE PROCEDURE Insert_User
	@Id INT,
	@Name VARCHAR(100),
    @Password VARCHAR(100),
    @LocationId INT,
    @Admin BIT,
    @Status BIT
AS
BEGIN

	INSERT INTO [User]
	VALUES (
        @Name,
        @Password,
        @LocationId,
        @Admin,
        @Status)

END