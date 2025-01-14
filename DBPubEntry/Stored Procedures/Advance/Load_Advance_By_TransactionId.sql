CREATE PROCEDURE Load_Advance_By_TransactionId
	@TransactionId int
AS
BEGIN
	
	SELECT
		*
	FROM Advance at
	WHERE at.TransactionId = @TransactionId

END;