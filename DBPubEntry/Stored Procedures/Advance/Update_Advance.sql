CREATE PROCEDURE Update_Advance
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

	UPDATE Advance
	SET 
	    LocationId = @LocationId,
	    PersonId = @PersonId,
	    AdvanceDate = @AdvanceDate,
	    Booking = @Booking,
		ApprovedBy = @ApprovedBy
	WHERE Id = @Id

END;