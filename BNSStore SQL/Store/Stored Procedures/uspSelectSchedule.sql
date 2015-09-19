CREATE PROCEDURE [Store].[uspSelectSchedule]
	@UserID int = NULL,
	@StartDate date = NULL,
	@EndDate date = NULL
AS
BEGIN
	SET NOCOUNT ON

	IF @StartDate IS NULL
	BEGIN
		SET @StartDate = GETDATE();
	END

	IF @EndDate IS NULL
	BEGIN
		SET @EndDate = CAST('9999/12/31' as date)
	END

	IF @UserID IS NULL
	BEGIN
		SELECT [Date], UserID, Store FROM Store.Schedule WHERE [Date] >= @StartDate AND [Date] <= @EndDate ORDER BY [Date]
	END
	ELSE
	BEGIN
		SELECT [Date], UserID, Store FROM Store.Schedule WHERE [Date] >= @StartDate AND [Date] <= @EndDate AND UserID = @UserID ORDER BY [Date]
	END
END

