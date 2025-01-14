CREATE PROCEDURE Update_Advance
	@Id INT,
	@LocationId INT,
	@PersonId INT,
	@DateTime DATETIME,
	@AdvanceDate DATE,
	@Booking INT,
	@ApprovedBy VARCHAR(50),
	@TransactionId INT
AS
BEGIN

	UPDATE Advance
	SET Booking = @Booking,
		ApprovedBy = @ApprovedBy
	WHERE Id = @Id

END;