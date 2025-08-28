CREATE PROCEDURE Insert_Transaction
	@Id INT OUTPUT,
    @PersonId INT,
    @Male INT,
    @Female INT,
    @Cash INT,
    @Card INT,
    @UPI INT,
    @Amex INT,
    @OnlineQR INT,
    @ReservationTypeId INT,
	@DateTime DATETIME,
    @ApprovedBy VARCHAR(250),
    @LocationId INT,
    @UserId INT
AS
BEGIN

    IF @Id = 0
    BEGIN
        INSERT INTO [Transaction]
        (
            PersonId,
            Male,
            Female,
            Cash,
            Card,
            UPI,
            Amex,
            OnlineQR,
            ReservationTypeId,
            ApprovedBy,
            LocationId,
            UserId
        )
        VALUES (
            @PersonId,
            @Male,
            @Female,
            @Cash,
            @Card,
            @UPI,
            @Amex,
            @OnlineQR,
            @ReservationTypeId,
            @ApprovedBy,
            @LocationId,
            @UserId
        )

        SET @Id = SCOPE_IDENTITY();
    END
    ELSE
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
            OnlineQR = @OnlineQR,
            ReservationTypeId = @ReservationTypeId,
            ApprovedBy = @ApprovedBy,
            LocationId = @LocationId,
            UserId = @UserId
        WHERE Id = @Id
    END
	SELECT @Id AS Id;

END;