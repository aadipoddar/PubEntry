CREATE PROCEDURE Insert_Person
	@Id INT OUTPUT,
	@Name VARCHAR(250),
	@Number VARCHAR(20),
	@Loyalty BIT
AS
BEGIN

	INSERT INTO Person(
		Name,
		Number,
		Loyalty
	) 
	OUTPUT INSERTED.Id
	VALUES (
		@Name,
		@Number,
		@Loyalty
	)

END;