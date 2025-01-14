CREATE PROCEDURE Update_Transaction
	@Id INT,
    @PersonId INT,
    @Male INT,
    @Female INT,
    @Cash INT,
    @Card INT,
    @UPI INT,
    @Amex INT,
    @ReservationType INT,
	@DateTime DATETIME,
    @ApprovedBy VARCHAR(50),
    @LocationId INT,
    @UserId INT
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [Transaction]
    SET
        PersonId = @PersonId,
        Male = @Male,
        Female = @Female,
        Cash = @Cash,
        Card = @Card,
        UPI = @UPI,
        Amex = @Amex,
        [ReservationTypeId] = @ReservationType,
        ApprovedBy = @ApprovedBy,
        LocationId = @LocationId,
        UserId = @UserId
    OUTPUT INSERTED.Id
    WHERE Id = @Id

END;