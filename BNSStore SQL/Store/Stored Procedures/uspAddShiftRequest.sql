CREATE PROCEDURE [Store].[uspAddShiftRequest]
	 @SenderID int = NULL,
	 @RecieverID int = NULL,
	 @Date date = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Scheudle TABLE([Date] date, [UserID] int, [Store] char);

	INSERT INTO @Scheudle([Date], [UserID], [Store]) EXEC [Store].[uspSelectSchedule] @UserID = @SenderID;

	IF(SELECT [UserID] FROM @Scheudle WHERE [Date] = @Date AND [UserID] = @SenderID) IS NULL
	BEGIN
		RAISERROR('Cannot find SenderID in Schedule on Date', 16, 1);
		RETURN;
	END;

	INSERT INTO @Scheudle([Date], [UserID], [Store]) EXEC [Store].[uspSelectSchedule] @UserID = @RecieverID;

	IF(SELECT [UserID] FROM @Scheudle WHERE [Date] = @Date AND [UserID] = @RecieverID) IS NOT NULL
	BEGIN
		RAISERROR('Found RecieverID in Schedule on Date', 16, 1);
		RETURN;
	END;

	INSERT INTO [Store].[ShiftRequest]([SenderID], [RecieverID], [Date]) VALUES(@SenderID, @RecieverID, @Date);
END;
