CREATE PROCEDURE Load_AdvanceDetail_By_AdvanceId
	@AdvanceId INT
AS
BEGIN

	SELECT * FROM AdvanceDetail WHERE AdvanceId = @AdvanceId

END