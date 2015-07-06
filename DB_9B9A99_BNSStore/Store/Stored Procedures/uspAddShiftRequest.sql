-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspAddShiftRequest]
	 @SenderID int = NULL,
	 @SenderUsername varchar(100) = NULL,
	 @RecieverID int = NULL,
	 @RecieverUsername varchar(100) = NULL,
	 @Date date = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

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

	    DECLARE @IsEmployee bit;
	    EXEC [Store].[uspIsEmployee]
		    @UserID = @SenderID,
		    @IsEmployee = @IsEmployee OUTPUT;
	    IF @IsEmployee = 0
		   BEGIN
			  RAISERROR('Not Employee', 16, 1);
			  RETURN;
		   END;
	    EXEC [Store].[uspIsEmployee]
		    @UserID = @RecieverID,
		    @IsEmployee = @IsEmployee OUTPUT;
	    IF @IsEmployee = 0
		   BEGIN
			  RAISERROR('Not Employee', 16, 1);
			  RETURN;
		   END;

	    DECLARE @Scheudle TABLE([Date] date, [UserID] int, [Store] char);

	    INSERT INTO @Scheudle([Date], [UserID], [Store])
	    EXEC [Store].[uspSelectSchedule]
		    @UserID = @SenderID;
	    IF(SELECT [UserID] FROM @Scheudle
		  WHERE [Date] = @Date AND
			   [UserID] = @SenderID) IS NULL
		   BEGIN
			  RAISERROR('Cannot find SenderID in Schedule on Date', 16, 1);
			  RETURN;
		   END;

	    INSERT INTO @Scheudle([Date], [UserID], [Store])
	    EXEC [Store].[uspSelectSchedule]
		    @UserID = @RecieverID;
	    IF(SELECT [UserID] FROM @Scheudle
		  WHERE [Date] = @Date AND
			   [UserID] = @RecieverID) IS NOT NULL
		   BEGIN
			  RAISERROR('Found RecieverID in Schedule on Date', 16, 1);
			  RETURN;
		   END;

	    INSERT INTO [Store].[ShiftRequest]([SenderID], [RecieverID], [Date])
	    VALUES(@SenderID, @RecieverID, @Date);
	END;
