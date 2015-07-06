-- =============================================
-- Author:		Cosh_
-- Create date: 2014.11.6
-- Description:	Add Translation
-- =============================================

CREATE PROCEDURE [Store].[uspSelectShiftRequest]
	  @RecieverID int = NULL,
	  @RecieverUsername varchar(100) = NULL,
	  @Date date = NULL
AS
	BEGIN
	    -- SET NOCOUNT ON added to prevent extra result sets from
	    -- interfering with SELECT statements.

	    SET NOCOUNT ON;

	    -- Insert statements for procedure here

	    IF @RecieverID IS NULL
		   BEGIN EXEC [User].[uspGetUserID]
				    @Username = @RecieverUsername,
				    @UserID = @RecieverID OUTPUT;
		   END;
		  
		  SELECT * FROM Store.ShiftRequest WHERE RecieverID = @RecieverID AND [Date] >= CONVERT(date, GETDATE());
	END;
