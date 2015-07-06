-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspDeclineShiftRequest]
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
		   
		   IF(SELECT [Date] FROM Store.ShiftRequest WHERE SenderID = @SenderID AND RecieverID = @RecieverID AND [Date] = @Date) IS NULL
		   BEGIN
			 RAISERROR('Cannot find ShiftRequest', 16, 1)
			 RETURN;
		   END
		   DELETE FROM Store.ShiftRequest WHERE SenderID = @SenderID AND RecieverID = @RecieverID AND [Date] = @Date;
	END;
