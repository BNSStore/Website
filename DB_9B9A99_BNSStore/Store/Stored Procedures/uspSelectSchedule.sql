-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation

-- =============================================
CREATE PROCEDURE [Store].[uspSelectSchedule]
	@UserID int = NULL,
	@Username varchar(100) = NULL,
	@StartDate date = NULL,
	@EndDate date = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @UserID IS NULL AND @Username IS NOT NULL
	BEGIN
		EXEC [User].uspGetUserID @Username = @Username, @UserID = @UserID OUTPUT
	END

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

GO
GRANT EXECUTE
    ON OBJECT::[Store].[uspSelectSchedule] TO [db_executor];

