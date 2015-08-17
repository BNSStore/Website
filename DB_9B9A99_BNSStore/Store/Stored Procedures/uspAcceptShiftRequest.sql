CREATE PROCEDURE [Store].[uspAcceptShiftRequest]
	 @SenderID int = NULL,
	 @SenderUsername varchar(100) = NULL,
	 @RecieverID int = NULL,
	 @RecieverUsername varchar(100) = NULL,
	 @Date date = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @SenderID IS NULL
		BEGIN EXEC [User].[uspGetUserID]
				@Username = @SenderUsername,
				@UserID = @SenderID OUTPUT;
		END;
	IF @RecieverID IS NULL
		BEGIN EXEC [User].[uspGetUserID]
				@Username = @RecieverUsername,
				@UserID = @RecieverID OUTPUT;
		END;

	IF(SELECT [Date] FROM [Store].[ShiftRequest]
		WHERE [SenderID] = @SenderID AND
			[RecieverID] = @RecieverID AND
			[Date] = @Date) IS NULL
		BEGIN
			RAISERROR('Cannot find ShiftRequest', 16, 1);
			RETURN;
		END;

	DECLARE @Store char;
	SET @Store = (SELECT [Store] FROM [Store].[Schedule]
				WHERE [UserID] = @SenderID AND
					[Date] = @Date);
	EXEC [Store].[uspAddShift]
		@UserID = @RecieverID,
		@Date = @Date,
		@Store = @Store;
	EXEC [Store].[uspDelShift]
		@UserID = @SenderID,
		@Date = @Date;

	DELETE FROM [Store].[ShiftRequest]
	WHERE [SenderID] = @SenderID AND
		[Date] = @Date;
	DELETE FROM [Store].[ShiftRequest]
	WHERE [RecieverID] = @RecieverID AND
		[Date] = @Date;
END;
