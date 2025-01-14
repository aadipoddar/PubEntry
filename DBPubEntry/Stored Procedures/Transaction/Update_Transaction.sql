CREATE PROCEDURE Update_Transaction
	@Id INT,
    @PersonId INT,
    @Male INT,
    @Female INT,
    @Cash INT,
    @Card INT,
    @UPI INT,
    @Amex INT,
    @ReservationTypeId INT,
	@DateTime DATETIME,
    @ApprovedBy VARCHAR(50),
    @LocationId INT,
    @UserId INT
AS
BEGIN

    UPDATE [Transaction]
    SET
        PersonId = @PersonId,
        Male = @Male,
        Female = @Female,
        Cash = @Cash,
        Card = @Card,
        UPI = @UPI,
        Amex = @Amex,
        ReservationTypeId = @ReservationTypeId,
        ApprovedBy = @ApprovedBy,
        LocationId = @LocationId,
        UserId = @UserId
    WHERE Id = @Id

END;