CREATE PROCEDURE Insert_AdvanceDetail
	@Id INT,
	@AdvanceId INT,
	@PaymentModeId INT,
	@Amount INT
AS
BEGIN

	INSERT INTO AdvanceDetail
		(AdvanceId,
		PaymentModeId,
		Amount)
	VALUES
		(@AdvanceId,
		@PaymentModeId,
		@Amount)

END;