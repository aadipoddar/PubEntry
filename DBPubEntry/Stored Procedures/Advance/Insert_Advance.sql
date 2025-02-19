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

	INSERT INTO Advance
		(LocationId,
		PersonId,
		AdvanceDate,
		Booking,
		ApprovedBy,
		UserId,
		TransactionId)
	OUTPUT INSERTED.Id
	VALUES
		(@LocationId,
		@PersonId,
		@AdvanceDate,
		@Booking,
		@ApprovedBy,
		@UserId,
		@TransactionId)

END;