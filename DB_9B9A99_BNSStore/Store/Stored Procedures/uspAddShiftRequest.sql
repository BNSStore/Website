CREATE PROCEDURE [Store].[uspAddShiftRequest]
	 @SenderID int = NULL,
	 @SenderUsername varchar(100) = NULL,
	 @RecieverID int = NULL,
	 @RecieverUsername varchar(100) = NULL,
	 @Date date = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @SenderID IS NULL
	BEGIN
		EXEC [User].[uspGetUserID] @Username = @SenderUsername, @UserID = @SenderID OUTPUT;
	END

	IF @RecieverID IS NULL
	BEGIN
		EXEC [User].[uspGetUserID] @Username = @RecieverUsername, @UserID = @RecieverID OUTPUT;
	END

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
