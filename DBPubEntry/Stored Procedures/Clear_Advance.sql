CREATE PROCEDURE [Clear_Advance]
    @AdvanceId INT,
	@TransactionId INT
AS
BEGIN

    UPDATE Advance
    SET TransactionId = @TransactionId
    WHERE Id = @AdvanceId

END;