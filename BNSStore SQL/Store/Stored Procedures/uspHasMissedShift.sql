CREATE PROCEDURE [Store].[uspHasMissedShift]
	  @UserID int = NULL,
	  @HasMissedShift bit = NULL OUTPUT
AS
BEGIN

	SET NOCOUNT ON

	IF(SELECT [UserID] FROM [Store].[Schedule] WHERE [UserID] = @UserID AND [Mark] = 0) IS NULL
	BEGIN
		SET @HasMissedShift = 0
	END
	ELSE
	BEGIN
		SET @HasMissedShift = 1
	END

END