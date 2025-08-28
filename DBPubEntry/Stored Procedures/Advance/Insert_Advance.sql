CREATE PROCEDURE Insert_Advance
	@Id INT,
	@LocationId INT,
	@PersonId INT,
	@DateTime DATETIME,
	@AdvanceDate DATE,
	@Booking INT,
	@ApprovedBy VARCHAR(50),
	@UserId INT,
	@TransactionId INT
AS
BEGIN

	IF @Id = 0
	BEGIN
		INSERT INTO Advance
			(LocationId,
			PersonId,
			AdvanceDate,
			Booking,
			ApprovedBy,
			UserId,
			TransactionId)
		VALUES
			(@LocationId,
			@PersonId,
			@AdvanceDate,
			@Booking,
			@ApprovedBy,
			@UserId,
			@TransactionId)

		SET @Id = SCOPE_IDENTITY();
	END

	ELSE
	BEGIN
		UPDATE Advance
		SET
			LocationId = @LocationId,
			PersonId = @PersonId,
			AdvanceDate = @AdvanceDate,
			Booking = @Booking,
			ApprovedBy = @ApprovedBy,
			UserId = @UserId,
			TransactionId = @TransactionId
		WHERE Id = @Id
	END

	SELECT @Id AS Id;

END;