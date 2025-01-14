CREATE PROCEDURE Delete_AdvanceDetails
	@AdvanceId INT
AS
BEGIN

	DELETE FROM AdvanceDetail WHERE AdvanceId = @AdvanceId

END;