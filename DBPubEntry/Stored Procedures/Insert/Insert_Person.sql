CREATE PROCEDURE Insert_Person
	@Id INT OUTPUT,
	@Name VARCHAR(250),
	@Number VARCHAR(20),
	@Loyalty BIT
AS
BEGIN

	IF @Id = 0
	BEGIN
		INSERT INTO Person(
			Name,
			Number,
			Loyalty
		) 
		VALUES (
			@Name,
			@Number,
			@Loyalty
		)

		SET @Id = SCOPE_IDENTITY();
	END

	ELSE
	BEGIN
		UPDATE Person
		SET
			Name = @Name,
			Number = @Number,
			Loyalty = @Loyalty
		WHERE Id = @Id
	END

	SELECT @Id AS Id;

END;