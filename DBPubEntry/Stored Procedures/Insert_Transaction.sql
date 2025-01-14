CREATE PROCEDURE Insert_Transaction
	@Id INT OUTPUT,
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

    SET NOCOUNT ON;

    INSERT INTO [Transaction](
        PersonId,
        Male,
        Female,
        Cash,
        Card,
        UPI,
        Amex,
        [ReservationTypeId],
        ApprovedBy,
        LocationId,
        UserId
    )
    OUTPUT INSERTED.Id
    VALUES (
        @PersonId,
        @Male,
        @Female,
        @Cash,
        @Card,
        @UPI,
        @Amex,
        @ReservationTypeId,
        @ApprovedBy,
        @LocationId,
        @UserId
    )

END;